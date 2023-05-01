namespace RolePlayReady.Models;

public abstract record Persisted : Base, IKey {
    public Guid Id { get; init; } = Guid.NewGuid();
    public State State { get; init; }
}
