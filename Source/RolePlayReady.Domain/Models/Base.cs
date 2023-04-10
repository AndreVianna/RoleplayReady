namespace RolePlayReady.Models;

public abstract record Base : Persistent, IBase {

    protected Base(IDateTime? dateTime = null)
        : base(dateTime) {
    }

    public required string Name { get; init; }
    public required string Description { get; init; }

    public string? ShortName { get; init; }

    public override string DataFileName => ShortName ?? Name.ToAcronym();

    public IList<string> Tags { get; init; } = new List<string>();

    public sealed override string ToString() => $"[{GetType().Name}] {Name}{(ShortName is not null ? $" ({ShortName})" : string.Empty)}";
}