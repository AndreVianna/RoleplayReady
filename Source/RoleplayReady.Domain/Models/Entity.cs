namespace RolePlayReady.Models;

public abstract record Entity : IEntity {
    private readonly string? _shortName;

    protected Entity(IDateTimeProvider? dateTime = null) {
        dateTime ??= new SystemDateTimeProvider();
        Timestamp = dateTime.Now;
    }

    public string? ShortName {
        get => _shortName;
        init => _shortName = Throw.IfNullOrWhiteSpaces(value);
    }

    // Setting, Owner, and Name must be unique.
    public required string Name { get; init; }
    public required string Description { get; init; }
    public string FullName => $"{GetType().Name} -> {Name} ({ShortName})";


    public IList<string> Tags { get; init; } = new List<string>();
    public IList<IAttribute> Attributes { get; init; } = new List<IAttribute>();

    public DateTime Timestamp { get; init; }
    public State State { get; init; } = State.NotReady;
}
