using RolePlayReady.Utilities;

namespace RolePlayReady.Operations.Assertions;

public record IsLess<TValue> : EntityAssertion
    where TValue : IComparable<TValue> {
    [SetsRequiredMembers]
    public IsLess(string attributeName, TValue maximum)
        : base(e => {
            var value = e.GetValue<TValue>(attributeName);
            return value is not null && value.CompareTo(maximum) < 0;
        }) {
    }
}