namespace RoleplayReady.Domain.Models.Modifiers;

public record IsLess<TValue> : Modifier
    where TValue : IComparable<TValue> {
    public IsLess(IHasModifiers target, string attributeName, TValue maximum, string message, ValidationSeverityLevel severityLevel = Hint)
        : base(target, e => {
            e.Validations.Add(new() {
                Severity = severityLevel,
                Validate = x => {
                    var attribute = x.GetAttribute<TValue>(attributeName);
                    return attribute is not null && attribute.Value is not null && attribute.Value.CompareTo(maximum) < 0;
                },
                Message = message,
            });
            return e;
        }) {
    }
}