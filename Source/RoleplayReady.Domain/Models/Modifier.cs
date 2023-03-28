namespace RoleplayReady.Domain.Models;

public record Modifier
{
    public required System System { get; init; }
    public required string Name { get; init; }
    public string Description { get; init; } = string.Empty;
    public Func<Entity, bool> IsApplicable { get; init; } = _ => true;
    public Func<Entity, Entity> Apply { get; init; } = _ => _;
    public IReadOnlyList<ElementValidation> Validations { get; init; } = new List<ElementValidation>();
}
