namespace RoleplayReady.Domain.Models;

public abstract record Entity : IEntity {
    protected Entity() {

    }

    [SetsRequiredMembers]
    protected Entity(string ownerId, string abbreviation, string name, string description, State? state = null) {
        OwnerId = ownerId ?? throw new ArgumentNullException(nameof(ownerId));
        Abbreviation = abbreviation ?? throw new ArgumentNullException(nameof(abbreviation));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        State = state ?? State.New;
    }

    [SetsRequiredMembers]
    protected Entity(IEntity parent, string ownerId, string abbreviation, string name, string description, State? state = null) :
        this(ownerId, abbreviation, name, description, state) {
        Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        State = state ?? parent.State;
    }

    [SetsRequiredMembers]
    protected Entity(string ownerId, string name, string description, State? state = null) :
        this(ownerId, name.ToAcronym(), name, description, state) {
    }

    [SetsRequiredMembers]
    protected Entity(IEntity parent, string ownerId, string name, string description, State? state = null) :
        this(ownerId, name.ToAcronym(), name, description, state) {
        Parent = parent ?? throw new ArgumentNullException(nameof(parent));
    }

    public IEntity? Parent { get; init; } // Top level should be a RuleSet.
    public IRuleSet? RuleSet { get; init; } // Quick access to the root.

    public required string OwnerId { get; init; }
    // RuleSet, OwnerId, and Name must be unique.
    public required string Name { get; init; }
    // RuleSet, OwnerId, and Abbreviation must be unique.
    public required string Abbreviation { get; init; }
    public required string Description { get; init; }

    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    public required State State { get; init; }
}
