using System.Validations.Extensions;

namespace RolePlayReady.Models.Attributes;

public record AttributeDefinition : IAttributeDefinition, IValidatable {
    public const int MaxNameSize = 100;
    public required string Name { get; init; }

    public const int MaxDescriptionSize = 1000;
    public required string Description { get; init; }

    public const int MaxShortNameSize = 10;
    public string? ShortName { get; init; }

    public required Type DataType { get; init; }

    public sealed override string ToString() => $"[{GetType().Name}] {Name}{(ShortName is not null ? $" ({ShortName})" : string.Empty)}: {DataType.Name}";

    public ICollection<IAttributeConstraint> Constraints { get; } = new List<IAttributeConstraint>();
    
    public ValidationResult Validate() {
        var result = new ValidationResult();
        result += Name.IsNotNull().And.NotEmptyOrWhiteSpace().And.NoLongerThan(MaxNameSize).Result;
        result += Description.IsNotNull().And.NotEmptyOrWhiteSpace().And.NoLongerThan(MaxDescriptionSize).Result;
        result += ShortName.IsNullOr().NotEmptyOrWhiteSpace().And.NoLongerThan(MaxShortNameSize).Result;
        return result;
    }
}