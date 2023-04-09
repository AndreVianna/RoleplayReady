namespace System;

[ExcludeFromCodeCoverage]
public class DefaultFileSystem : IFileSystem {
    public string CombinePath(string firstPath, string secondPath)
        => Path.Combine(firstPath, secondPath);

    public string ExtractFileNameFrom(string filePath)
        => Path.GetFileName(filePath);

    public string[] GetFilesFrom(string folderPath, string searchPattern, SearchOption searchOptions)
        => Directory.GetFiles(folderPath, searchPattern, searchOptions);

    public void MoveFile(string sourcePath, string targetPath)
        => File.Move(sourcePath, targetPath);

    public Stream OpenFileForReading(string filePath)
        => new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

    public Stream CreateNewFileAndOpenForWriting(string filePath)
        => new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
}