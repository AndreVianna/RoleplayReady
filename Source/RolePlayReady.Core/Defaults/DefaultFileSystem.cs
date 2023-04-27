namespace System.Defaults;

[ExcludeFromCodeCoverage]
public class DefaultFileSystem : IFileSystem {
    public string CombinePath(params string[] paths)
        => Path.Combine(paths);

    public string ExtractFileNameFrom(string filePath)
        => Path.GetFileName(filePath);

    public string[] GetFilesFrom(string folderPath, string searchPattern, SearchOption searchOptions)
        => Directory.GetFiles(folderPath, searchPattern, searchOptions);

    public void CreateFolderIfNotExists(string folderPath)
        => Directory.CreateDirectory(folderPath);

    public bool FileExists(string filePath)
        => File.Exists(filePath);

    public void MoveFile(string sourcePath, string targetPath)
        => File.Move(sourcePath, targetPath);

    public Stream OpenFileForReading(string filePath)
        => new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

    public Stream CreateNewFileAndOpenForWriting(string filePath)
        => new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
}