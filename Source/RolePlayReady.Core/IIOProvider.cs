namespace RolePlayReady;

public interface IIOProvider {
    string CombinePath(string firstPath, string secondPath);
    string[] GetFilesFrom(string folderPath, string searchPattern, SearchOption searchOptions);
    void MoveFile(string sourcePath, string targetPath);
    string ExtractFileNameFrom(string filePath);
    Stream OpenFileForReading(string filePath);
    Stream CreateNewFileAndOpenForWriting(string path);
}