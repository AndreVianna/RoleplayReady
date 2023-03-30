namespace RoleplayReady.Domain.Models;

public record Attribute : IAttribute
{
    public Attribute(Type type)
    {
        Type = type;
    }

    // RuleSet and Name must be unique.
    public required RuleSet RuleSet { get; init; }
    public required string Name { get; init; }
    public Type Type { get; }
    public string? Description { get; init; }
}