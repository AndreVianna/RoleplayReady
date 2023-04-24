namespace RolePlayReady.Models;

public record GameObject : Component, IGameObject {
    public required string Unit { get; init; }
}