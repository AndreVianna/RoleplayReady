using static System.Text.Json.JsonSerializer;

namespace RolePlayReady.DataAccess.Repositories;

public partial class TrackedJsonFileRepository : ITrackedJsonFileRepository {
    private readonly ILogger<TrackedJsonFileRepository> _logger;
    private readonly IFileSystem _io;
    private readonly IDateTime _dateTime;

    private readonly string _baseFolderPath;

    private const string _timestampFormat = "yyyyMMddHHmmss";
    private const string _baseFolderConfigurationKey = $"{nameof(TrackedJsonFileRepository)}:BaseFolder";

    public TrackedJsonFileRepository(IConfiguration configuration, IFileSystem? io, IDateTime? dateTime, ILoggerFactory? loggerFactory) {
        _logger = loggerFactory?.CreateLogger<TrackedJsonFileRepository>() ?? NullLogger<TrackedJsonFileRepository>.Instance;
        _io = io ?? new DefaultFileSystem();
        _dateTime = dateTime ?? new DefaultDateTime();
        var baseFolder = configuration[_baseFolderConfigurationKey];
        const string keyId = $"{nameof(configuration)}[{_baseFolderConfigurationKey}]";
        _baseFolderPath = Ensure.NotNullOrWhiteSpace(baseFolder, keyId).Trim();
    }

    public async Task<Result<IEnumerable<DataFile<TData>>>> GetAllAsync<TData>(string owner, string path, CancellationToken cancellation = default) {
        try {
            var folderPath = GetFolderFullPath(owner, path);
            _logger.LogDebug("Getting files from '{path}'...", folderPath);
            var filePaths = _io.GetFilesFrom(folderPath, "+*.json", SearchOption.TopDirectoryOnly);
            var fileInfos = new List<DataFile<TData>>();
            foreach (var filePath in filePaths) {
                var result = await GetFileDataOrDefaultAsync<TData>(filePath, cancellation).ConfigureAwait(false);
                if (result.HasValue)
                    fileInfos.Add(result.Value!);
            }

            _logger.LogDebug("{fileCount} files retrieved from '{path}'.", fileInfos.Count, folderPath);
            return fileInfos;
        }
        catch (Exception ex) {
            var errorFolder = $"{_baseFolderPath}/{path}";
            _logger.LogError(ex, "Failed to get files from '{path}'!", errorFolder);
            throw;
        }
    }

    public async Task<Maybe<DataFile<TData>>> GetByIdAsync<TData>(string owner, string path, string id, CancellationToken cancellation = default) {
        try {
            var folderPath = GetFolderFullPath(owner, path);
            _logger.LogDebug("Getting latest data from '{path}/{id}'...", folderPath, id);
            var filePath = GetActiveFile(folderPath, id);
            if (filePath is null) {
                _logger.LogDebug("File '{filePath}' not found.", filePath);
                return default(DataFile<TData>);
            }

            var result = await GetFileDataOrDefaultAsync<TData>(filePath, cancellation).ConfigureAwait(false);
            return result;
        }
        catch (Exception ex) {
            var errorFolder = $"{_baseFolderPath}/{path}";
            _logger.LogError(ex, "Failed to get data from file '{path}/{id}'!", errorFolder, id);
            throw;
        }
    }

    public async Task<Result<DateTime>> UpsertAsync<TData>(string owner, string path, string id, TData data, CancellationToken cancellation = default) {
        var now = _dateTime.Now;
        try {
            var folderPath = GetFolderFullPath(owner, path);
            _logger.LogDebug("Adding or updating data in '{path}/{id}'...", folderPath, id);
            var currentFile = GetActiveFile(folderPath, id);
            if (currentFile is not null)
                _io.MoveFile(currentFile, currentFile.Replace("+", ""));
            var newFilePath = _io.CombinePath(folderPath, $"+{id}_{now:yyyyMMddHHmmss}.json");
            await using var stream = _io.CreateNewFileAndOpenForWriting(newFilePath);
            await SerializeAsync(stream, data, cancellationToken: cancellation);

            _logger.LogDebug("Date for '{path}/{id}' added or updated.", folderPath, id);
        }
        catch (Exception ex) {
            var errorFolder = $"{_baseFolderPath}/{path}";
            _logger.LogError(ex, "Failed to add or update file '{path}/{id}'!", errorFolder, id);
            throw;
        }

        return now;
    }

    public Result<bool> Delete(string owner, string path, string id) {
        try {
            var folderPath = GetFolderFullPath(owner, path);
            _logger.LogDebug("Deleting file '{path}/{id}'...", folderPath, id);
            var activeFiles = _io.GetFilesFrom(folderPath, $"+{id}*.json", SearchOption.TopDirectoryOnly)
                .Union(_io.GetFilesFrom(folderPath, $"{id}*.json", SearchOption.TopDirectoryOnly)).ToArray();
            if (activeFiles.Length == 0) {
                _logger.LogDebug("File '{path}/{id}' not found.", folderPath, id);
                return false;
            }

            foreach (var activeFile in activeFiles) {
                var fileName = _io.ExtractFileNameFrom(activeFile);
                var deletedFile = _io.CombinePath(folderPath, $"-{fileName.Replace("+", string.Empty)}");
                _io.MoveFile(activeFile, deletedFile);
            }

            _logger.LogDebug("File '{path}/{id}' deleted.", folderPath, id);
            return true;
        }
        catch (Exception ex) {
            var errorFolder = $"{_baseFolderPath}/{path}";
            _logger.LogError(ex, "Failed to delete file '{path}/{id}'!", errorFolder, id);
            throw;
        }
    }

    private async Task<Maybe<DataFile<TData>>> GetFileDataOrDefaultAsync<TData>(string filePath, CancellationToken cancellation) {
        var result = default(DataFile<TData>);
        try {
            var fileName = _io.ExtractFileNameFrom(filePath);
            if (!TryParseFileName(fileName, out var fileInfo)) {
                _logger.LogWarning("File name '{filePath}' is invalid.", filePath);
                return default(DataFile<TData>);
            }

            await using var stream = _io.OpenFileForReading(filePath);
            var content = await DeserializeAsync<TData>(stream, cancellationToken: cancellation);
            _logger.LogDebug("Data from '{filePath}' retrieved.", filePath);
            result = new DataFile<TData>() {
                Name = fileInfo.Name,
                Timestamp = fileInfo.Timestamp,
                Content = content!
            };
        }
        catch (Exception ex) {
            _logger.LogWarning(ex, "File '{filePath}' content is invalid.", filePath);
        }

        return result;
    }

    private bool TryParseFileName(string fileName, out FileInfo fileInfo) {
        fileInfo = default!;
        var match = FileNameMatcher().Match(fileName);
        if (!match.Success || !TryParseTimestamp(match.Groups["datetime"].Value, out var dateTime))
            return false;
        fileInfo = new() { Name = match.Groups["id"].Value, Timestamp = dateTime };
        return true;
    }

    private bool TryParseTimestamp(string value, out DateTime timestamp)
        => _dateTime
            .TryParseExact(value, _timestampFormat, null, DateTimeStyles.None, out timestamp);

    private record FileInfo {
        public required string Name { get; init; }
        public required DateTime Timestamp { get; init; }
    }

    private string GetFolderFullPath(string owner, string path)
        => _io.CombinePath(_baseFolderPath, owner.Trim(), path.Trim());

    private string? GetActiveFile(string folder, string id)
        => _io.GetFilesFrom(folder, $"+{id}*.json", SearchOption.TopDirectoryOnly)
        .FirstOrDefault();

    [GeneratedRegex("^\\+(?<id>[a-zA-Z0-9]+)_(?<datetime>\\d{14})\\.json$", RegexOptions.Compiled, "en-CA")]
    private static partial Regex FileNameMatcher();
}
