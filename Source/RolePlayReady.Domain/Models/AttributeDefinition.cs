using System.Validators.Abstractions;
using System.Validators.Extensions;

namespace RolePlayReady.Models;

public record AttributeDefinition : IAttributeDefinition, IValidatable {
    public const int MaxNameSize = 100;
    public required string Name { get; init; }

    public const int MaxDescriptionSize = 1000;
    public required string Description { get; init; }

    public const int MaxShortNameSize = 10;
    public string? ShortName { get; init; }

    public required Type DataType { get; init; }

    public sealed override string ToString() => $"[{GetType().Name}] {Name}{(ShortName is not null ? $" ({ShortName})" : string.Empty)}: {DataType.Name}";

    public Validation Validate() {
        var result = new Validation();
        result += Name.Is().NotNull().And.NotEmptyOrWhiteSpace().And.NoLongerThan(MaxNameSize).Result;
        result += Description.Is().NotNull().And.NotEmptyOrWhiteSpace().And.NoLongerThan(MaxDescriptionSize).Result;
        result += ShortName.Is().NotEmptyOrWhiteSpace().And.NoLongerThan(MaxShortNameSize).Result;
        result += DataType.Is().NotNull().Result;
        return result;
    }
}