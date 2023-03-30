namespace RoleplayReady.Domain.Models.Effects;

public record IsGrater<TValue> : Effect
    where TValue : IComparable<TValue> {
    public IsGrater(IHasEffects parent, string attributeName, TValue minimum, string message, Severity severity = Suggestion)
        : base(parent, e => {
            e.Validations.Add(new() {
                Parent = e,
                Severity = severity,
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