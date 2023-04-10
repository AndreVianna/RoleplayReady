namespace RolePlayReady.Models;

public record GameSetting : Entity, IGameSetting {
    public required IList<IAttribute> AttributeDefinitions { get; init; } = new List<IAttribute>();
}