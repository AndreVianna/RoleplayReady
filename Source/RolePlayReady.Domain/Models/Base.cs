using static RolePlayReady.Constants.Constants.Validation.Definition;

namespace RolePlayReady.Models;

public record Base : IBase, IValidatable {
    public required string Name { get; init; }
    public required string Description { get; init; }
    public string? ShortName { get; init; }

    public ICollection<string> Tags { get; init; } = new List<string>();

    public virtual Result Validate() {
        var result = new Result();
        result += Name.IsNotNull().And.IsNotEmptyOrWhiteSpace().And.MaximumLengthIs(MaximumNameLength).Result;
        result += Description.IsNotNull().And.IsNotEmptyOrWhiteSpace().And.MaximumLengthIs(MaximumDescriptionLength).Result;
        result += ShortName.IsNullOr().IsNotEmptyOrWhiteSpace().And.MaximumLengthIs(MaximumShortNameLength).Result;
        result += Tags.ForEach(item => item.IsNotNull().And.IsNotEmptyOrWhiteSpace().And.MaximumLengthIs(MaximumTagLength)).Result;
        return result;
    }
}