namespace RoleplayReady.Domain.Models;

public record GameSystem
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Abbreviation { get; init; }
    public required string Publisher { get; init; }

    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    public bool IsDeleted { get; init; }
}