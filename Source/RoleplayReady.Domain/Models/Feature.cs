namespace RoleplayReady.Domain.Models;

public record Feature : IHasEffects {
    public Feature(IHasFeatures parent, string name, string description) {
        Parent = parent;
        Name = name;
        Description = description;
    }

    public Feature(IHasFeatures parent, string name) {
        Parent = parent;
        Name = name;
    }

    public IHasFeatures Parent { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public IList<Effect> Effects { get; init; } = new List<Effect>();
}