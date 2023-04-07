namespace RolePlayReady.Models;

public record RuleSet : IRuleSet {
    public RuleSet() { }

    [SetsRequiredMembers]
    public RuleSet(string abbreviation, string name, string description, IDateTimeProvider? dateTime = null) {
        Abbreviation = Throw.IfNullOrWhiteSpaces(abbreviation);
        Name = Throw.IfNullOrWhiteSpaces(name);
        Description = Throw.IfNull(description);

        dateTime ??= new SystemDateTimeProvider();
        Timestamp = dateTime.Now;
        State = State.NotReady;
    }

    public INode Root => this;
    public INode? Parent => default;
    public IList<INode> Children { get; init; } = new List<INode>();

    public string OwnerId { get; init; } = "System";

    public string EntityType => GetType().Name;
    // RuleSet, OwnerId, and Abbreviation must be unique.
    public required string Abbreviation { get; init; }
    // RuleSet, OwnerId, and Name must be unique.
    public required string Name { get; init; }
    public required string Description { get; init; }
    public string FullName => $"<{EntityType}> {Name} ({Abbreviation})";


    //public Usage Usage { get; init; }

    public required IList<string> Tags { get; init; } = new List<string>();
    public required IList<IAttribute> Attributes { get; init; } = new List<IAttribute>();

    public DateTime Timestamp { get; init; }
    public State State { get; init; }
}