namespace RoleplayReady.Domain.Models.Modifiers;

public record Contains<TValue> : Modifier {
    public Contains(IHasModifiers target, string attributeName, TValue candidate, string message, ValidationSeverityLevel severityLevel = Hint)
        : base(target, e => {
            e.Validations.Add(new() {
                Severity = severityLevel,
                Validate = x => {
                    var attribute = x.GetAttribute<HashSet<TValue>>(attributeName);
                    return attribute?.Value != null && attribute.Value.Contains(candidate);
                },
                Message = message,
            });
            return e;
        }) {
    }
}