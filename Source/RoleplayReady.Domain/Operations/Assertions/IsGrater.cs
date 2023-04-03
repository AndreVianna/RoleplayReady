using RolePlayReady.Utilities;

namespace RolePlayReady.Operations.Assertions;

public record IsGrater<TValue> : EntityAssertion
    where TValue : IComparable<TValue> {
    [SetsRequiredMembers]
    public IsGrater(string attributeName, TValue minimum)
        : base(e => {
            var value = e.GetValue<TValue>(attributeName);
            return value is not null && value.CompareTo(minimum) > 0;
        }) {
    }
}