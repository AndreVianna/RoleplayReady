namespace RolePlayReady.Models;

public record GameSystemSetting : Entity<Guid>, IGameSystemSetting {
    public GameSystemSetting(IDateTime? dateTime = null)
        : base(dateTime) {
    }

    public IList<IAttributeDefinition> AttributeDefinitions { get; init; } = new List<IAttributeDefinition>();

    public override ValidationResult Validate() {
        var result = base.Validate();
        result += AttributeDefinitions.ForEach(item => item.IsNotNull().And.IsValid()).Result;
        return result;
    }
}