using System.Abstractions;

using Microsoft.Extensions.Logging.Abstractions;

using static System.Text.Json.JsonSerializer;

namespace RolePlayReady.DataAccess.Repositories;

public partial class DataFileRepository : IDataFileRepository {
    private readonly ILogger<DataFileRepository> _logger;
    private readonly IFileSystem _io;
    private readonly IDateTime _dateTime;

    private readonly string _baseFolderPath;

    private const string _timestampFormat = "yyyyMMddHHmmss";
    private const string _baseFolderConfigurationKey = $"{nameof(DataFileRepository)}:BaseFolder";
    private const string _errorMessage = $"{_baseFolderConfigurationKey} configuration value is missing.";

    public DataFileRepository(IConfiguration configuration, IFileSystem? io, IDateTime? dateTime, ILoggerFactory? loggerFactory) {
        _logger = loggerFactory?.CreateLogger<DataFileRepository>() ?? NullLogger<DataFileRepository>.Instance;
        _io = io ?? new DefaultFileSystem();
        _dateTime = dateTime ?? new DefaultDateTime();
        var baseFolder = configuration[_baseFolderConfigurationKey];
        _baseFolderPath = Throw.IfNullOrWhiteSpaces(baseFolder, _errorMessage, nameof(configuration)).Trim();
    }

    public async Task<IEnumerable<DataFile<TData>>> GetAllAsync<TData>(string owner, string path, CancellationToken cancellation = default) {
        try {
            var folderPath = GetFolderFullPath(owner, path);
            _logger.LogDebug("Getting data files from '{path}'...", folderPath);
            var filePaths = _io.GetFilesFrom(folderPath, "+*.json", SearchOption.TopDirectoryOnly);
            var fileInfos = new List<DataFile<TData>>();
            foreach (var filePath in filePaths) {
                var result = await GetFileDataAsync<TData>(filePath, cancellation).ConfigureAwait(false);
                if (!result.IsSuccessful) {
                    _logger.LogWarning(result.Exception, "Data file '{filePath}' is invalid.", filePath);
                    continue;
                }

                fileInfos.Add(result.Value);
            }

            _logger.LogDebug("{fileCount} data files retrieved from '{path}'.", fileInfos.Count, folderPath);
            return fileInfos;
        }
        catch (Exception ex) {
            var errorFolder = $"{_baseFolderPath}/{path}";
            _logger.LogError(ex, "Failed to get data files from '{path}'!", errorFolder);
            return Array.Empty<DataFile<TData>>();
        }
    }

    public async Task<DataFile<TData>?> GetByIdAsync<TData>(string owner, string path, string id, CancellationToken cancellation = default) {
        try {
            var folderPath = GetFolderFullPath(owner, path);
            _logger.LogDebug("Getting data from file '{path}/{id}'...", folderPath, id);
            var filePath = _io.GetFilesFrom(folderPath, $"+{id}*.json", SearchOption.TopDirectoryOnly)
                .FirstOrDefault();

            if (filePath is null) {
                _logger.LogDebug("Data file '{path}/{id}' not found.", folderPath, id);
                return null;
            }


            var result = await GetFileDataAsync<TData>(filePath, cancellation).ConfigureAwait(false);
            if (!result.IsSuccessful) {
                _logger.LogWarning(result.Exception, "Data file '{filePath}' is invalid.", filePath);
                return null;
            }

            _logger.LogDebug("Data from '{path}/{id}' retrieved.", folderPath, id);
            return result.Value;
        }
        catch (Exception ex) {
            var errorFolder = $"{_baseFolderPath}/{path}";
            _logger.LogError(ex, "Failed to get data from file '{path}/{id}'!", errorFolder, id);
            return null;
        }
    }

    public async Task<bool> UpsertAsync<TData>(string owner, string path, string id, TData data, CancellationToken cancellation = default) {
        try {
            var folderPath = GetFolderFullPath(owner, path);
            _logger.LogDebug("Adding or updating data file '{path}/{id}'...", folderPath, id);
            var currentFile = _io.GetFilesFrom(folderPath, $"+{id}*.json", SearchOption.TopDirectoryOnly)
                .FirstOrDefault();
            if (currentFile is not null)
                _io.MoveFile(currentFile, currentFile.Replace("+", ""));
            var filePath = _io.CombinePath(folderPath, $"+{id}_{_dateTime.Now:yyyyMMddHHmmss}.json");
            await using var stream = _io.CreateNewFileAndOpenForWriting(filePath);
            await SerializeAsync(stream, data, cancellationToken: cancellation);

            _logger.LogDebug("Data file '{path}/{id}' added or updated.", folderPath, id);
        }
        catch (Exception ex) {
            var errorFolder = $"{_baseFolderPath}/{path}";
            _logger.LogError(ex, "Failed to add or update data file '{path}/{id}'!", errorFolder, id);
            return false;
        }
        return true;
    }

    public bool Delete(string owner, string path, string id) {
        try {
            var folderPath = GetFolderFullPath(owner, path);
            _logger.LogDebug("Deleting data file '{path}/{id}'...", folderPath, id);
            var activeFiles = _io.GetFilesFrom(folderPath, $"+{id}*.json", SearchOption.TopDirectoryOnly)
                .Union(_io.GetFilesFrom(folderPath, $"{id}*.json", SearchOption.TopDirectoryOnly));
            foreach (var activeFile in activeFiles) {
                var fileName = _io.ExtractFileNameFrom(activeFile);
                var deletedFile = _io.CombinePath(folderPath, $"-{fileName.Replace("+", string.Empty)}");
                _io.MoveFile(activeFile, deletedFile);
            }

            _logger.LogDebug("Data file '{path}/{id}' deleted.", folderPath, id);
            return true;
        }
        catch (Exception ex) {
            var errorFolder = $"{_baseFolderPath}/{path}";
            _logger.LogError(ex, "Failed to delete data file '{path}/{id}'!", errorFolder, id);
            return false;
        }
    }

    private async Task<ValueOf<DataFile<TData>>> GetFileDataAsync<TData>(string filePath, CancellationToken cancellation) {
        ValueOf<DataFile<TData>> result;
        try {
            var fileName = _io.ExtractFileNameFrom(filePath);
            if (!TryParseFileName(fileName, out var fileInfo))
                return new InvalidOperationException($"'{filePath}' is an invalid file name.");
            await using var stream = _io.OpenFileForReading(filePath);
            var content = await DeserializeAsync<TData>(stream, cancellationToken: cancellation);
            result = new DataFile<TData> {
                Name = fileInfo.Name,
                Timestamp = fileInfo.Timestamp,
                Content = content!
            };
        }
        catch (Exception ex) {
            result = ex;
        }

        return result;
    }

    private bool TryParseFileName(string fileName, out FileInfo fileInfo) {
        fileInfo = default!;
        var match = FileNameMatcher().Match(fileName);
        if (!match.Success || !TryParseTimestamp(match.Groups["datetime"].Value, out var dateTime))
            return false;
        fileInfo = new FileInfo { Name = match.Groups["id"].Value, Timestamp = dateTime };
        return true;
    }

    private bool TryParseTimestamp(string value, out DateTime timestamp)
        => _dateTime
            .TryParseExact(value, _timestampFormat, null, DateTimeStyles.None, out timestamp);

    private record FileInfo {
        public required string Name { get; init; }
        public required DateTime Timestamp { get; init; }
    }

    private string GetFolderFullPath(string owner, string path) => _io.CombinePath(_baseFolderPath, owner.Trim(), path.Trim());

    [GeneratedRegex("^\\+(?<id>[a-zA-Z0-9]+)_(?<datetime>\\d{14})\\.json$", RegexOptions.Compiled, "en-CA")]
    private static partial Regex FileNameMatcher();

}
