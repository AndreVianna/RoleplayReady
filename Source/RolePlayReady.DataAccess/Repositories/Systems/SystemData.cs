namespace RolePlayReady.DataAccess.Repositories.Systems;

public record SystemData : IKey  {
    public Guid Id { get; init; }
    public State State { get; init; }
    public string? ShortName { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string[] Tags { get; init; }
}
