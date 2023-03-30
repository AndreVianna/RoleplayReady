namespace RoleplayReady.Domain.Models.Effects;

public record IsEqual<TValue> : Effect
    where TValue : IEquatable<TValue> {
    public IsEqual(IHasEffects parent, string attributeName, TValue validValue, string message, Severity severity = Suggestion)
        : base(parent, e => {
            e.Validations.Add(new() {
                Parent = e,
                Severity = severity,
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