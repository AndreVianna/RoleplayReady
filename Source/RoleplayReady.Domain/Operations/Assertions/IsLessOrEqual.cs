namespace RoleplayReady.Domain.Operations.Assertions;

public record IsLessOrEqual<TValue> : EntityAssertion
    where TValue : IComparable<TValue> {
    [SetsRequiredMembers]
    public IsLessOrEqual(string attributeName, TValue maximum)
        : base(e => {
            var value = e.GetValue<TValue>(attributeName);
            return value is not null && value.CompareTo(maximum) <= 0;
        }) {
    }
}