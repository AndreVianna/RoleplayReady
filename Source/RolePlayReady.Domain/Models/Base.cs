using System.Validators.Extensions;

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

    public virtual Validation Validate() {
        var result = new Validation();
        result += Name.ValueIs().NotNull().And.NotEmptyOrWhiteSpace().And.NoLongerThan(MaxNameSize).Result;
        result += Description.ValueIs().NotNull().And.NotEmptyOrWhiteSpace().And.NoLongerThan(MaxDescriptionSize).Result;
        result += ShortName.ValueIs().NotEmptyOrWhiteSpace().And.NoLongerThan(MaxShortNameSize).Result;
        result += Tags.CollectionIs().NotNull().And.EachItem(t => t.ValueIs().NotNull().And.NoLongerThan(MaxTagSize)).Result;
        return result;
    }

    public sealed override string ToString() => $"[{GetType().Name}] {Name}{(ShortName is not null ? $" ({ShortName})" : string.Empty)}";
}
