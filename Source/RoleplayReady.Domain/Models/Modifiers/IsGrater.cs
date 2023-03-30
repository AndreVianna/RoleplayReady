namespace RoleplayReady.Domain.Models.Modifiers;

public record IsGrater<TValue> : Modifier
    where TValue : IComparable<TValue> {
    public IsGrater(IHasModifiers target, string attributeName, TValue minimum, string message, ValidationSeverityLevel severityLevel = Hint)
        : base(target, e => {
            e.Validations.Add(new() {
                Severity = severityLevel,
                Validate = x => {
                    var attribute = x.GetAttribute<TValue>(attributeName);
                    return attribute is not null && attribute.Value is not null && attribute.Value.CompareTo(minimum) > 0;
                },
                Message = message,
            });
            return e;
        }) {
    }
}