using System.Validations.Extensions;

namespace RolePlayReady.Models;

public record GameSystemSetting : Entity<Guid>, IGameSystemSetting {
    public GameSystemSetting(IDateTime? dateTime = null)
        : base(dateTime) {
    }

    public IList<IAttributeDefinition> AttributeDefinitions { get; init; } = new List<IAttributeDefinition>();

    public override ValidationResult Validate() {
        var result = base.Validate();
        result += AttributeDefinitions.IsNotNull().And.ForEach(item => item.IsNotNull().And.Valid()).Result;
        return result;
    }
}