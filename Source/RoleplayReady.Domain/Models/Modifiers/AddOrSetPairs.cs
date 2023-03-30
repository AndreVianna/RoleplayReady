namespace RoleplayReady.Domain.Models.Modifiers;

public record AddOrSetPairs<TKey, TValue> : Modifier
    where TKey : notnull {
    public AddOrSetPairs(IHasModifiers target, string attributeName, Func<IHasAttributes, IEnumerable<KeyValuePair<TKey, TValue>>> getItemsFrom)
        : base(target, e => {
        var dictionary = e.GetAttribute<Dictionary<TKey, TValue>>(attributeName)?.Value;
        if (dictionary is null)
            return e;
        foreach (var item in getItemsFrom(e))
            dictionary[item.Key] = item.Value;
        return e;
    }) {
    }
}