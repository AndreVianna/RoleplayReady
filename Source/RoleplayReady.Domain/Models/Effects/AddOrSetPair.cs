namespace RoleplayReady.Domain.Models.Effects;

public record AddOrSetPair<TKey, TValue> : Effect
    where TKey : notnull {
    public AddOrSetPair(IHasEffects parent, string attributeName, TKey key, Func<IHasAttributes, TValue> getValueFrom)
        : base(parent, e => {
        var dictionary = e.GetAttribute<Dictionary<TKey, TValue>>(attributeName)?.Value;
        if (dictionary is null)
            return e;
        dictionary[key] = getValueFrom(e);
        return e;
    }) {
    }
}