using System.Validations.Extensions;

using RolePlayReady.Models.Attributes;

namespace RolePlayReady.Models;

public record GameSystemSetting : Entity<Guid>, IGameSystemSetting {
    public GameSystemSetting(IDateTime? dateTime = null)
        : base(dateTime) {
    }

    public IList<AttributeDefinition> AttributeDefinitions { get; init; } = new List<AttributeDefinition>();

    public override ValidationResult Validate() {
        var result = base.Validate();
        result += AttributeDefinitions.CollectionItems().Each(i => i.ValueIs().NotNull().And.Valid()).Result;
        return result;
    }
}