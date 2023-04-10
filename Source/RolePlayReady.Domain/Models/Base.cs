namespace RolePlayReady.Models;

public abstract record Base : Persistent, IBase {

    protected Base(IDateTime? dateTime = null) {
        dateTime ??= new DefaultDateTime();
        Timestamp = dateTime.Now;
    }

    public required string Name { get; init; }
    public required string Description { get; init; }

    public string? ShortName { get; init; }

    public override string DataFileName => ShortName ?? Name.ToAcronym() ?? throw new InvalidOperationException("Data file id not defined.");

    public IList<string> Tags { get; init; } = new List<string>();

    public override string ToString() => $"[{GetType().Name}] {Name}{(ShortName is not null ? $" ({ShortName})" : string.Empty)}";
}