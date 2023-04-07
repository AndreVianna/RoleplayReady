namespace RolePlayReady.DataAccess.Repositories;

public class DataFileRepository
    : IDataFileRepository {
    private static readonly Regex _fileNameMatcher = new(@"^\+(?<id>[a-zA-Z0-9]+)_(?<datetime>\d{8}T\d{6})$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private readonly string _baseFolderPath;

    public DataFileRepository(IConfiguration configuration) {
        _baseFolderPath = configuration[$"{nameof(DataFileRepository)}:BaseFolder"]
                          ?? throw new InvalidOperationException($"{nameof(DataFileRepository)}:BaseFolder configuration value is missing.");
    }

    public async Task<IEnumerable<DataFile<TData>>> GetAllAsync<TData>(string path, CancellationToken cancellation = default) {
        var folderPath = Path.Combine(_baseFolderPath, Throw.IfNull(path));
        var filePaths = Directory.GetFiles(folderPath, "+*.json", SearchOption.TopDirectoryOnly);
        var fileInfos = new List<DataFile<TData>>();
        foreach (var filePath in filePaths) {
            var fileInfo = await GetFileData<TData>(filePath, cancellation).ConfigureAwait(false);
            fileInfos.Add(fileInfo);
        }

        return fileInfos;
    }

    public Task<IEnumerable<DataFile<TData>>> GetAllAsync<TData>(CancellationToken cancellation = default)
        => GetAllAsync<TData>(string.Empty, cancellation);

    public async Task<DataFile<TData>?> GetByIdAsync<TData>(string path, string id, CancellationToken cancellation = default) {
        var folderPath = Path.Combine(_baseFolderPath, Throw.IfNull(path));
        var filePath = Directory
            .GetFiles(folderPath, $"+{id}*.json", SearchOption.TopDirectoryOnly)
            .FirstOrDefault();
        return filePath is not null
            ? await GetFileData<TData>(filePath, cancellation)
            : null;
    }

    public Task<DataFile<TData>?> GetByIdAsync<TData>(string id, CancellationToken cancellation = default)
        => GetByIdAsync<TData>(string.Empty, id, cancellation);

    public async Task UpsertAsync<TData>(string path, string id, TData data, CancellationToken cancellation = default) {
        var folderPath = Path.Combine(_baseFolderPath, Throw.IfNull(path));
        var currentFile = Directory
            .GetFiles(folderPath, $"+{id}*.json", SearchOption.TopDirectoryOnly)
            .FirstOrDefault();
        if (currentFile is not null)
            File.Move(currentFile, currentFile.Replace("!", ""));
        var filePath = Path.Combine(folderPath, $"!{id}_{DateTime.UtcNow:yyyyMMdd'T'HHmmss}.json");
        await using var stream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.None);
        await JsonSerializer.SerializeAsync(stream, data, cancellationToken: cancellation);
    }

    public Task UpsertAsync<TData>(string id, TData data, CancellationToken cancellation = default)
        => UpsertAsync(string.Empty, id, data, cancellation);

    public void Delete(string path, string id) {
        var folderPath = Path.Combine(_baseFolderPath, Throw.IfNull(path));
        var activeFiles = Directory.GetFiles(folderPath, $"+{id}*.json", SearchOption.TopDirectoryOnly)
            .Union(Directory.GetFiles(folderPath, $"{id}*.json", SearchOption.TopDirectoryOnly));
        foreach (var activeFile in activeFiles)
            File.Move(activeFile, $"-{activeFile}");
    }

    public void Delete(string id) => Delete(string.Empty, id);

    private static async Task<DataFile<TData>> GetFileData<TData>(string filePath, CancellationToken cancellation) {
        var fileName = Path.GetFileName(filePath);
        var (name, dateTime) = ParseInput(fileName);
        await using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        var content = await JsonSerializer.DeserializeAsync<TData>(stream, cancellationToken: cancellation)
                      ?? throw new InvalidDataException($"Failed to read content from file '{fileName}'.");
        return new DataFile<TData> {
            Id = name,
            Timestamp = dateTime,
            Content = content
        };
    }

    private static (string Name, DateTime DateTime) ParseInput(string input) {
        var match = _fileNameMatcher.Match(input);
        return match.Success
            ? DateTime.TryParseExact(match.Groups["datetime"].Value, "yyyyMMddTHHmmss", null, System.Globalization.DateTimeStyles.None, out var dateTime)
                ? (match.Groups["id"].Value, dateTime)
                : throw new FormatException($"Invalid date time value in the file id: {input}")
            : throw new FormatException($"The file id doesn't match the expected format: {input}");
    }
}