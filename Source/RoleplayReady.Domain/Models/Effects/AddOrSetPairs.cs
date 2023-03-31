namespace RoleplayReady.Domain.Models.Effects;

public record AddOrSetPairs<TKey, TValue> : Effect
    where TKey : notnull {
    [SetsRequiredMembers]
    public AddOrSetPairs(string attributeName, Func<IElement, IEnumerable<KeyValuePair<TKey, TValue>>> getItemsFrom)
        : base(e => {
        var dictionary = e.GetAttribute<Dictionary<TKey, TValue>>(attributeName)?.Value;
        if (dictionary is null)
            return e;
        foreach (var item in getItemsFrom(e))
            dictionary[item.Key] = item.Value;
        return e;
    }) {
    }
}