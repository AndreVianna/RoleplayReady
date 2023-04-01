namespace RoleplayReady.Domain.Models.Checkers;

public record IsGraterOrEqualChecker<TValue> : ElementChecker
    where TValue : IComparable<TValue> {
    [SetsRequiredMembers]
    public IsGraterOrEqualChecker(string attributeName, TValue minimum)
        : base(e => {
            var attribute = e.GetAttribute<TValue>(attributeName);
            return attribute.Value is not null && attribute.Value.CompareTo(minimum) >= 0;
        }) {
    }
}