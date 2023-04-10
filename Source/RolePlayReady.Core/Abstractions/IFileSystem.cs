namespace System.Abstractions;

public interface IFileSystem {
    string CombinePath(params string[] paths);
    string[] GetFilesFrom(string folderPath, string searchPattern, SearchOption searchOptions);
    bool FileExists(string filePath);
    void MoveFile(string sourcePath, string targetPath);
    string ExtractFileNameFrom(string filePath);
    Stream OpenFileForReading(string filePath);
    Stream CreateNewFileAndOpenForWriting(string path);
}