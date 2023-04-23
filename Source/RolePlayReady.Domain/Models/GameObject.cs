namespace RolePlayReady.Models;

public record GameObject : Entity, IGameObject {
    public required string Unit { get; init; }
}