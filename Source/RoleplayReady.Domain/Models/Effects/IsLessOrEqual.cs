namespace RoleplayReady.Domain.Models.Effects;

public record IsLessOrEqual<TValue> : Effect
    where TValue : IComparable<TValue> {
    public IsLessOrEqual(IHasEffects parent, string attributeName, TValue maximum, string message, Severity severity = Suggestion)
        : base(parent, e => {
            e.Validations.Add(new() {
                Parent = e,
                Severity = severity,
                Validate = x => {
                    var attribute = x.GetAttribute<TValue>(attributeName);
                    return attribute is not null && attribute.Value is not null && attribute.Value.CompareTo(maximum) <= 0;
                },
                Message = message,
            });
            return e;
        }) {
    }
}