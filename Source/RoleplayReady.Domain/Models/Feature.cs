namespace RoleplayReady.Domain.Models;

public record Feature : IHasModifiers {
    public Feature(IHasFeatures target, string name, string description) {
        Target = target;
        Name = name;
        Description = description;
    }

    public Feature(IHasFeatures target, string name) {
        Target = target;
        Name = name;
    }

    public IHasFeatures Target { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public IList<Modifier> Modifiers { get; init; } = new List<Modifier>();
}