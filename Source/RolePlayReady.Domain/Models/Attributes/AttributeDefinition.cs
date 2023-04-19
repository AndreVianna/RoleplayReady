namespace RolePlayReady.Models.Attributes;

public record AttributeDefinition : IAttributeDefinition {
    public const int MaxNameSize = 100;
    public required string Name { get; init; }

    public const int MaxDescriptionSize = 1000;
    public required string Description { get; init; }

    public const int MaxShortNameSize = 10;
    public string? ShortName { get; init; }

    public required Type DataType { get; init; }

    public sealed override string ToString() => $"[{nameof(AttributeDefinition)}] {Name}{(ShortName is not null ? $" ({ShortName})" : string.Empty)}: {DataType.GetFriendlyName()}";

    public ICollection<IAttributeConstraint> Constraints { get; } = new List<IAttributeConstraint>();
    
    public ValidationResult Validate() {
        var result = new ValidationResult();
        result += Name.IsNotNull().And.IsNotEmptyOrWhiteSpace().And.MaximumLengthIs(MaxNameSize).Result;
        result += Description.IsNotNull().And.IsNotEmptyOrWhiteSpace().And.MaximumLengthIs(MaxDescriptionSize).Result;
        result += ShortName.IsNullOr().IsNotEmptyOrWhiteSpace().And.MaximumLengthIs(MaxShortNameSize).Result;
        return result;
    }
}