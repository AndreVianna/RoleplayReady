namespace RolePlayReady.DataAccess.Models;

public record DataFile<TData> {
    public required string Id { get; set; }
    public required DateTime Timestamp { get; set; }
    public required TData Content { get; set; }
}