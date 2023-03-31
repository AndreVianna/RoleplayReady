namespace RoleplayReady.Domain.Models.Effects;

public record ContainsKey<TKey, TValue> : Effect
    where TKey : notnull {
    [SetsRequiredMembers]
    public ContainsKey(string attributeName, TKey key, string message)
        : base(e => {
            e.Validations.Add(new Validation(x => {
                    var attribute = x.GetAttribute<Dictionary<TKey, TValue>>(attributeName);
                    return attribute?.Value != null && attribute.Value.ContainsKey(key);
                },
                message));
            return e;
        }) {
    }
}