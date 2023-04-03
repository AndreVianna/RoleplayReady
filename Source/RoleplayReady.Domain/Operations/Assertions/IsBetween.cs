using RolePlayReady.Utilities;

namespace RolePlayReady.Operations.Assertions;

public record IsBetween<TValue> : EntityAssertion
    where TValue : IComparable<TValue> {
    [SetsRequiredMembers]
    public IsBetween(string attributeName, TValue minimum, TValue maximum)
        : base(e => {
            var value = e.GetValue<TValue>(attributeName);
            return value is not null
                   && value.CompareTo(minimum) >= 0
                   && value.CompareTo(maximum) <= 0;
        }) {
    }
}