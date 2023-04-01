namespace RoleplayReady.Domain.Models.Modifiers;

public record AddOrSetPairModifier<TKey, TValue> : ElementModifier
    where TKey : notnull {
    [SetsRequiredMembers]
    public AddOrSetPairModifier(string attributeName, TKey key, Func<IElement, TValue> getValueFrom)
        : base(e => {
            var dictionary = e.GetAttribute<Dictionary<TKey, TValue>>(attributeName)?.Value;
            if (dictionary is null)
                return e;
            dictionary[key] = getValueFrom(e);
            return e;
        }) {
    }
}