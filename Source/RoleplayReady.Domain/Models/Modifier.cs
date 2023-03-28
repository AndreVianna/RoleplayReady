namespace RoleplayReady.Domain.Models;

public record Modifier
{
    public required GameSystem GameSystem { get; init; }
    public required string Name { get; init; }
    public string Description { get; init; } = string.Empty;
    public Func<GameEntity, bool> IsApplicable { get; init; } = _ => true;
    public Func<GameEntity, GameEntity> Apply { get; init; } = _ => _;
    public IReadOnlyList<Validation> Validations { get; init; } = new List<Validation>();
}
