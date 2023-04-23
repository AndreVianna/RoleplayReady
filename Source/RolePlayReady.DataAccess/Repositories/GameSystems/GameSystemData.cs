namespace RolePlayReady.DataAccess.Repositories.GameSystems;

public record GameSystemData {
    public string? ShortName { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string[] Tags { get; init; }
    public required string[] Domains { get; init; }
}
