namespace RolePlayReady.Models.Abstractions;

public interface IPersistent<out TKey> {
    TKey Id { get; }
    DateTime Timestamp { get; }
    State State { get; }
}