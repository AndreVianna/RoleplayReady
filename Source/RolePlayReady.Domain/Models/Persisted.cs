namespace RolePlayReady.Models;

public abstract record Persisted : Base, IPersisted {
    public Guid Id { get; init; } = Guid.NewGuid();
    public State State { get; init; } = State.New;
}
