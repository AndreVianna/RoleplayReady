using static RolePlayReady.Constants.Constants.Validation.AttributeDefinition;

namespace RolePlayReady.Models.Attributes;

public record AttributeDefinition : IAttributeDefinition {
    public required string Name { get; init; }
    public required string Description { get; init; }
    public string? ShortName { get; init; }
    public required Type DataType { get; init; }

    public sealed override string ToString() => $"[{nameof(AttributeDefinition)}] {Name}{(ShortName is not null ? $" ({ShortName})" : string.Empty)}: {DataType.GetName()}";

    public ICollection<IAttributeConstraint> Constraints { get; } = new List<IAttributeConstraint>();

    public ValidationResult Validate() {
        var result = new ValidationResult();
        result += Name.IsNotNull().And.IsNotEmptyOrWhiteSpace().And.MaximumLengthIs(MaxNameSize).Result;
        result += Description.IsNotNull().And.IsNotEmptyOrWhiteSpace().And.MaximumLengthIs(MaxDescriptionSize).Result;
        result += ShortName.IsNullOr().IsNotEmptyOrWhiteSpace().And.MaximumLengthIs(MaxShortNameSize).Result;
        return result;
    }
}