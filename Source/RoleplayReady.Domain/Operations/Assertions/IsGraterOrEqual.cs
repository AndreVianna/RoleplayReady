using RolePlayReady.Utilities;

namespace RolePlayReady.Operations.Assertions;

public record IsGraterOrEqual<TValue> : EntityAssertion
    where TValue : IComparable<TValue> {
    [SetsRequiredMembers]
    public IsGraterOrEqual(string attributeName, TValue minimum)
        : base(e => {
            var value = e.GetValue<TValue>(attributeName);
            return value is not null && value.CompareTo(minimum) >= 0;
        }) {
    }
}