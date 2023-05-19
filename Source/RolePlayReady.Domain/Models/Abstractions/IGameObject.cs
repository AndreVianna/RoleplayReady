namespace RolePlayReady.Models.Abstractions;

public interface IGameObject : IPersisted {
    string Unit { get; }
}