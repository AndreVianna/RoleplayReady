namespace RoleplayReady.Domain.Operations.Assertions;

public record IsEqual<TValue> : EntityAssertion
    where TValue : IEquatable<TValue> {
    [SetsRequiredMembers]
    public IsEqual(string attributeName, TValue validValue)
        : base(e => {
            var value = e.GetValue<TValue>(attributeName);
            return value is not null && value.Equals(validValue);
        }) {
    }
}