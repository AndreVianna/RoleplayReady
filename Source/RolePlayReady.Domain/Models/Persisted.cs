namespace RolePlayReady.Models;

public record Persisted<TBase> : IPersisted<TBase>
    where TBase : class {
    public Persisted(IDateTime? dateTime = null) {
        dateTime ??= new DefaultDateTime();
        Timestamp = dateTime.Now;
    }
    public required Guid Id { get; init; }
    public DateTime Timestamp { get; init; }
    public State State { get; init; } = State.Pending;
    public required TBase Content { get; init; }

    public override string ToString() => $"{Content}: {State}, {Id}, {Timestamp:yyyy-MM-dd HH:mm:ss.fffffff}";
}
