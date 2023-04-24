namespace RolePlayReady.Models.Abstractions;

public interface IPersisted : IKey {
    State State { get; }
}