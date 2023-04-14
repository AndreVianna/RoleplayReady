using System.Validators.Extensions;

namespace RolePlayReady.Models;

public record GameSystemSetting : Entity<Guid>, IGameSystemSetting {
    public GameSystemSetting(IDateTime? dateTime = null)
        : base(dateTime) {
    }

    public IList<AttributeDefinition> AttributeDefinitions { get; init; } = new List<AttributeDefinition>();

    public override Validation Validate() {
        var result = base.Validate();
        result += AttributeDefinitions.ListIs().NotNull().And.EachItem(i => i.Is().NotNull().And.Valid()).Result;
        return result;
    }
}