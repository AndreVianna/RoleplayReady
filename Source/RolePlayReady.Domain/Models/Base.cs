namespace RolePlayReady.Models;

public abstract record Base<TKey> : Persistent<TKey>, IBase<TKey> {

    protected Base(IDateTime? dateTime = null)
        : base(dateTime) {
    }

    public required string Name { get; init; }
    public required string Description { get; init; }

    public string? ShortName { get; init; }

    public IList<string> Tags { get; init; } = new List<string>();

    public sealed override string ToString() => $"[{GetType().Name}] {Name}{(ShortName is not null ? $" ({ShortName})" : string.Empty)}";
}