namespace RoleplayReady.Domain.Models.Effects;

public record ContainsKey<TKey, TValue> : Effect
    where TKey : notnull {
    public ContainsKey(IHasEffects parent, string attributeName, TKey key, string message, Severity severity = Suggestion)
        : base(parent, e => {
            e.Validations.Add(new() {
                Parent = e,
                Severity = severity,
                Validate = x => {
                    var attribute = x.GetAttribute<Dictionary<TKey, TValue>>(attributeName);
                    return attribute?.Value != null && attribute.Value.ContainsKey(key);
                },
                Message = message,
            });
            return e;
        }) {
    }
}