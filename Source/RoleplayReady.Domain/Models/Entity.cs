namespace RoleplayReady.Domain.Models;

public abstract record Entity : IEntity {
    protected Entity() {

    }

    [SetsRequiredMembers]
    protected Entity(string ownerId, string name, string? description = null) {
        OwnerId = ownerId ?? throw new ArgumentNullException(nameof(ownerId));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
    }

    // RuleSet, OwnerId, and Name must be unique.
    public required string OwnerId { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
}
