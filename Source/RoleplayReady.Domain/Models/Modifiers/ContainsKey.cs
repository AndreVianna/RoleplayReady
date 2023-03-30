namespace RoleplayReady.Domain.Models.Modifiers;

public record ContainsKey<TKey, TValue> : Modifier
    where TKey : notnull {
    public ContainsKey(IHasModifiers target, string attributeName, TKey key, string message, ValidationSeverityLevel severityLevel = Hint)
        : base(target, e => {
            e.Validations.Add(new() {
                Severity = severityLevel,
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