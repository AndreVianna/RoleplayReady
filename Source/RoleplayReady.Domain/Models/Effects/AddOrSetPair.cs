namespace RoleplayReady.Domain.Models.Effects;

public record AddOrSetPair<TKey, TValue> : Effect
    where TKey : notnull {
    [SetsRequiredMembers]
    public AddOrSetPair(string attributeName, TKey key, Func<IElement, TValue> getValueFrom)
        : base(e => {
        var dictionary = e.GetAttribute<Dictionary<TKey, TValue>>(attributeName)?.Value;
        if (dictionary is null)
            return e;
        dictionary[key] = getValueFrom(e);
        return e;
    }) {
    }
}