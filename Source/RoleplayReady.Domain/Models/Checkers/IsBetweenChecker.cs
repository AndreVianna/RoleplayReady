namespace RoleplayReady.Domain.Models.Checkers;

public record IsBetweenChecker<TValue> : ElementChecker
    where TValue : IComparable<TValue> {
    [SetsRequiredMembers]
    public IsBetweenChecker(string attributeName, TValue minimum, TValue maximum)
        : base(e => {
            var attribute = e.GetAttribute<TValue>(attributeName);
            return attribute.Value is not null
                   && attribute.Value.CompareTo(minimum) >= 0
                   && attribute.Value.CompareTo(maximum) <= 0;
        }) {
    }
}