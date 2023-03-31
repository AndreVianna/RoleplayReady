namespace RoleplayReady.Domain.Models.Effects;

public record IsEqual<TValue> : Effect
    where TValue : IEquatable<TValue> {
    [SetsRequiredMembers]
    public IsEqual(string attributeName, TValue validValue, string message)
        : base(e => {
            e.Validations.Add(new Validation(x => {
                    var attribute = x.GetAttribute<TValue>(attributeName);
                    return attribute is not null && attribute.Value is not null && attribute.Value.Equals(validValue);
                }, message));
            return e;
        }) {
    }
}