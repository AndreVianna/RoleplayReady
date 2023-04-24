using static System.Text.Json.JsonSerializer;

namespace RolePlayReady.DataAccess.Repositories;

public partial class TrackedJsonFileRepository<TData> : ITrackedJsonFileRepository<TData>
    where TData : class, IKey {
    private readonly ILogger<TrackedJsonFileRepository<TData>> _logger;
    private readonly IFileSystem _io;
    private readonly IDateTime _dateTimeProvider;

    private readonly string _baseFolderPath;

    private const string _timestampFormat = "yyyyMMddHHmmss";
    private const string _baseFolderConfigurationKey = $"{nameof(TrackedJsonFileRepository<TData>)}:BaseFolder";

    public TrackedJsonFileRepository(IConfiguration configuration, IFileSystem? io, IDateTime? dateTime, ILoggerFactory? loggerFactory) {
        _logger = loggerFactory?.CreateLogger<TrackedJsonFileRepository<TData>>() ?? NullLogger<TrackedJsonFileRepository<TData>>.Instance;
        _io = io ?? new DefaultFileSystem();
        _dateTimeProvider = dateTime ?? new DefaultDateTime();
        var baseFolder = configuration[_baseFolderConfigurationKey];
        const string keyId = $"{nameof(configuration)}[{_baseFolderConfigurationKey}]";
        _baseFolderPath = Ensure.IsNotNullOrWhiteSpace(baseFolder, keyId).Trim();
    }

    public async Task<IEnumerable<TData>> GetAllAsync(string owner, string path, CancellationToken cancellation = default) {
        try {
            var folderPath = GetFolderFullPath(owner, path);
            _logger.LogDebug("Getting files from '{path}'...", folderPath);
            var filePaths = _io.GetFilesFrom(folderPath, "+*.json", SearchOption.TopDirectoryOnly);
            var fileInfos = new List<TData>();
            foreach (var filePath in filePaths) {
                var result = await GetFileDataOrDefaultAsync(filePath, cancellation).ConfigureAwait(false);
                if (result is null) continue;
                fileInfos.Add(result);
            }

            _logger.LogDebug("{fileCount} files retrieved from '{path}'.", fileInfos.Count, folderPath);
            return fileInfos.ToArray();
        }
        catch (Exception ex) {
            var errorFolder = $"{_baseFolderPath}/{path}";
            _logger.LogError(ex, "Failed to get files from '{path}'!", errorFolder);
            throw;
        }
    }

    public async Task<TData?> GetByIdAsync(string owner, string path, Guid id, CancellationToken cancellation = default) {
        try {
            var folderPath = GetFolderFullPath(owner, path);
            _logger.LogDebug("Getting latest data from '{path}/{id}'...", folderPath, id);
            var filePath = GetActiveFile(folderPath, id);
            if (filePath is null) {
                _logger.LogDebug("File '{filePath}' not found.", filePath);
                return default;
            }

            return await GetFileDataOrDefaultAsync(filePath, cancellation).ConfigureAwait(false);
        }
        catch (Exception ex) {
            var errorFolder = $"{_baseFolderPath}/{path}";
            _logger.LogError(ex, "Failed to get data from file '{path}/{id}'!", errorFolder, id);
            throw;
        }
    }

    public async Task<TData> UpsertAsync(string owner, string path, TData data, CancellationToken cancellation = default) {
        try {
            var folderPath = GetFolderFullPath(owner, path);
            _logger.LogDebug("Adding or updating data in '{path}/{id}'...", folderPath, data.Id);
            var currentFile = GetActiveFile(folderPath, data.Id);
            if (currentFile is not null)
                _io.MoveFile(currentFile, currentFile.Replace("+", ""));
            var newFilePath = _io.CombinePath(folderPath, $"+{data.Id}_{_dateTimeProvider.Now:yyyyMMddHHmmss}.json");
            await WriteToFileAsync(data, newFilePath, cancellation);

            _logger.LogDebug("Date for '{path}/{id}' added or updated.", folderPath, data.Id);

            return await GetFileDataAsync(newFilePath, cancellation).ConfigureAwait(false);
        }
        catch (Exception ex) {
            var errorFolder = $"{_baseFolderPath}/{path}";
            _logger.LogError(ex, "Failed to add or update file '{path}/{id}'!", errorFolder, data.Id);
            throw;
        }
    }

    private Task WriteToFileAsync(TData data, string newFilePath, CancellationToken cancellation) {
        using var stream = _io.CreateNewFileAndOpenForWriting(newFilePath);
        return SerializeAsync(stream, data, cancellationToken: cancellation);
    }

    public Result<bool> Delete(string owner, string path, Guid id) {
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

    private async Task<TData?> GetFileDataOrDefaultAsync(string filePath, CancellationToken cancellation) {
        try {
            return await GetFileDataAsync(filePath, cancellation);
        }
        catch (Exception ex) {
            _logger.LogWarning(ex, "File '{filePath}' content is invalid.", filePath);
            return default;
        }
    }

    private async Task<TData> GetFileDataAsync(string filePath, CancellationToken cancellation) {
        var fileName = _io.ExtractFileNameFrom(filePath);
        if (!IsFileNameValid(fileName)) {
            throw new InvalidOperationException($"File name '{filePath}' is invalid.");
        }

        await using var stream = _io.OpenFileForReading(filePath);
        var content = await DeserializeAsync<TData>(stream, cancellationToken: cancellation);
        _logger.LogDebug("Data from '{filePath}' retrieved.", filePath);
        return content!;
    }

    private bool IsFileNameValid(string fileName) {
        var match = FileNameMatcher().Match(fileName);
        return match.Success && _dateTimeProvider
           .TryParseExact(match.Groups["datetime"].Value, _timestampFormat, null, DateTimeStyles.None, out _);
    }

    private string GetFolderFullPath(string owner, string path)
        => _io.CombinePath(_baseFolderPath, owner.Trim(), path.Trim());

    private string? GetActiveFile(string folder, Guid id)
        => _io.GetFilesFrom(folder, $"+{id}*.json", SearchOption.TopDirectoryOnly)
        .FirstOrDefault();

    [GeneratedRegex("^\\+(?<id>[a-zA-Z0-9-]{36})_(?<datetime>\\d{14})\\.json$", RegexOptions.Compiled, "en-CA")]
    private static partial Regex FileNameMatcher();
}
