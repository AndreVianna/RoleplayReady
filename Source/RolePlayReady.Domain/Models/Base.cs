using System.Collections.Generic;

using static RolePlayReady.Constants.Constants.Validation.Base;

namespace RolePlayReady.Models;

public abstract record Base<TKey> : Persistent<TKey>, IBase<TKey> {
    protected Base(IDateTime? dateTime = null)
        : base(dateTime) {
    }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public string? ShortName { get; init; }

    public ICollection<string> Tags { get; init; } = new List<string>();
    public IDictionary<string, int> Temp { get; init; } = new Dictionary<string, int>();

    public virtual ValidationResult Validate() {
        var result = new ValidationResult();
        result += Name.IsNotNull().And.IsNotEmptyOrWhiteSpace().And.MaximumLengthIs(MaxNameSize).Result;
        result += Description.IsNotNull().And.IsNotEmptyOrWhiteSpace().And.MaximumLengthIs(MaxDescriptionSize).Result;
        result += ShortName.IsNullOr().IsNotEmptyOrWhiteSpace().And.MaximumLengthIs(MaxShortNameSize).Result;
        result += Tags.ForEach(item => item.IsNotNull().And.IsNotEmptyOrWhiteSpace().And.MaximumLengthIs(MaxTagSize)).Result;
        return result;
    }

    public sealed override string ToString() => $"[{GetType().Name}] {Name}{(ShortName is not null ? $" ({ShortName})" : string.Empty)}";
}
