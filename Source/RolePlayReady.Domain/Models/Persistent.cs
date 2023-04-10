namespace RolePlayReady.Models;

public abstract record Persistent : IPersistent {

    protected Persistent(IDateTime? dateTime = null) {
        dateTime ??= new DefaultDateTime();
        Timestamp = dateTime.Now;
    }

    public DateTime Timestamp { get; init; }
    public State State { get; init; } = State.NotReady;
    public abstract string DataFileName { get; }
}