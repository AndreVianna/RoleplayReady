using static RolePlayReady.Constants.Constants.Validation.Definition;

namespace RolePlayReady.Models;

public record Base : IBase {
    public required string Name { get; init; }
    public required string Description { get; init; }
    public string? ShortName { get; init; }
    public ICollection<string> Tags { get; init; } = new List<string>();

    public override string ToString() => $"[{GetType().Name}] {Name}{(ShortName is not null ? $" ({ShortName})" : string.Empty)}";

    public virtual ValidationResult Validate() {
        var result = new ValidationResult();
        result += Name.IsNotNull().And.IsNotEmptyOrWhiteSpace().And.MaximumLengthIs(MaxNameSize).Result;
        result += Description.IsNotNull().And.IsNotEmptyOrWhiteSpace().And.MaximumLengthIs(MaxDescriptionSize).Result;
        result += ShortName.IsNullOr().IsNotEmptyOrWhiteSpace().And.MaximumLengthIs(MaxShortNameSize).Result;
        result += Tags.ForEach(item => item.IsNotNull().And.IsNotEmptyOrWhiteSpace().And.MaximumLengthIs(MaxTagSize)).Result;
        return result;
    }
}