namespace RolePlayReady.Models;

public record GameSystemSetting : Entity<Guid>, IGameSystemSetting {
    public GameSystemSetting(IDateTime? dateTime = null)
        : base(dateTime) {
    }

    public IList<AttributeDefinition> AttributeDefinitions { get; init; } = new List<AttributeDefinition>();
}