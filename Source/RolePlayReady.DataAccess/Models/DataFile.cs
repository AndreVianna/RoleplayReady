namespace RolePlayReady.DataAccess.Models;

public record DataFile<TData> : IDataFile<TData> {
    public required string Name { get; set; }
    public required DateTime Timestamp { get; set; }
    public required TData Content { get; set; }
}
