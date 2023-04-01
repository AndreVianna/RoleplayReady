namespace RoleplayReady.Domain.Models.Checkers;

public record IsLessOrEqualChecker<TValue> : ElementChecker
    where TValue : IComparable<TValue> {
    [SetsRequiredMembers]
    public IsLessOrEqualChecker(string attributeName, TValue maximum)
        : base(e => {
            var attribute = e.GetAttribute<TValue>(attributeName);
            return attribute.Value is not null && attribute.Value.CompareTo(maximum) <= 0;
        }) {
    }
}