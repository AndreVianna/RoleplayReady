namespace RolePlayReady.Models;

public record GameSystemSetting : Entity, IGameSetting {
    public GameSystemSetting(IDateTime? dateTime = null)
        : base(dateTime) {
    }

    public IList<IAttributeDefinition> AttributeDefinitions { get; init; } = new List<IAttributeDefinition>();
}