namespace RolePlayReady.Models;

public abstract record Persistent<TKey> : IPersistent<TKey> {

    protected Persistent(IDateTime? dateTime = null) {
        dateTime ??= new DefaultDateTime();
        Timestamp = dateTime.Now;
    }

    public required TKey Id { get; init; }
    public DateTime Timestamp { get; init; }
    public State State { get; init; } = State.Pending;
}