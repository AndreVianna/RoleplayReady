namespace RoleplayReady.Domain.Models.Modifiers;

public record IsEqual<TValue> : Modifier
    where TValue : IEquatable<TValue> {
    public IsEqual(IHasModifiers target, string attributeName, TValue validValue, string message, ValidationSeverityLevel severityLevel = Hint)
        : base(target, e => {
            e.Validations.Add(new() {
                Severity = severityLevel,
                Validate = x => {
                    var attribute = x.GetAttribute<TValue>(attributeName);
                    return attribute is not null && attribute.Value is not null && attribute.Value.Equals(validValue);
                },
                Message = message,
            });
            return e;
        }) {
    }
}