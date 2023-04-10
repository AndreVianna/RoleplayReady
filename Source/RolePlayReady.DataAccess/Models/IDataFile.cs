namespace RolePlayReady.DataAccess.Models;

public interface IDataFile<out TData> {
    string Name { get; }
    DateTime Timestamp { get; }
    TData Content { get; }
}