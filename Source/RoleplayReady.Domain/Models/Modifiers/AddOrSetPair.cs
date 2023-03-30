namespace RoleplayReady.Domain.Models.Modifiers;

public record AddOrSetPair<TKey, TValue> : Modifier
    where TKey : notnull {
    public AddOrSetPair(IHasModifiers target, string attributeName, TKey key, Func<IHasAttributes, TValue> getValueFrom)
        : base(target, e => {
        var dictionary = e.GetAttribute<Dictionary<TKey, TValue>>(attributeName)?.Value;
        if (dictionary is null)
            return e;
        dictionary[key] = getValueFrom(e);
        return e;
    }) {
    }
}