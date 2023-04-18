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
        result += Name.IsNotNull().And.NotEmptyOrWhiteSpace().And.NoLongerThan(MaxNameSize).Result;
        result += Description.IsNotNull().And.NotEmptyOrWhiteSpace().And.NoLongerThan(MaxDescriptionSize).Result;
        result += ShortName.IsNullOr().NotEmptyOrWhiteSpace().And.NoLongerThan(MaxShortNameSize).Result;
        result += Tags.IsNotNull().And.ForEach(item => item.IsNotNull().And.NoLongerThan(MaxTagSize)).Result;
        return result;
    }

    public sealed override string ToString() => $"[{GetType().Name}] {Name}{(ShortName is not null ? $" ({ShortName})" : string.Empty)}";
}
