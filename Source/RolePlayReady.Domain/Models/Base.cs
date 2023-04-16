using System.Validations.Extensions;

namespace RolePlayReady.Models;

public abstract record Base<TKey> : Persistent<TKey>, IBase<TKey> {

    protected Base(IDateTime? dateTime = null)
        : base(dateTime) {
    }

    public const int MaxNameSize = 100;
    public required string Name { get; init; }

    public const int MaxDescriptionSize = 1000;
    public required string Description { get; init; }

    public const int MaxShortNameSize = 10;
    public string? ShortName { get; init; }

    public const int MaxTagSize = 20;
    public IList<string> Tags { get; init; } = new List<string>();

    public virtual ValidationResult Validate() {
        var result = new ValidationResult();
        result += Name.ValueIs().NotNull().And.NotEmptyOrWhiteSpace().And.MaximumLengthOf(MaxNameSize).Result;
        result += Description.ValueIs().NotNull().And.NotEmptyOrWhiteSpace().And.MaximumLengthOf(MaxDescriptionSize).Result;
        result += ShortName.ValueIs().NotEmptyOrWhiteSpace().And.MaximumLengthOf(MaxShortNameSize).Result;
        result += Tags.CollectionItems().Each(t => t.ValueIs().NotNull().And.MaximumLengthOf(MaxTagSize)).Result;
        return result;
    }

    public sealed override string ToString() => $"[{GetType().Name}] {Name}{(ShortName is not null ? $" ({ShortName})" : string.Empty)}";
}
