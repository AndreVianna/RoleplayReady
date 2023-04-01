namespace RoleplayReady.Domain.Models.Checkers;

public record IsEqualChecker<TValue> : ElementChecker
    where TValue : IEquatable<TValue> {
    [SetsRequiredMembers]
    public IsEqualChecker(string attributeName, TValue validValue)
        : base(e => {
            var attribute = e.GetAttribute<TValue>(attributeName);
            return attribute.Value is not null && attribute.Value.Equals(validValue);
        }) {
    }
}