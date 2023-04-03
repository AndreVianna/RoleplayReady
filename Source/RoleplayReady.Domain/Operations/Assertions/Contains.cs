using RolePlayReady.Utilities;

namespace RolePlayReady.Operations.Assertions;

public record Contains<TValue> : EntityAssertion {
    [SetsRequiredMembers]
    public Contains(string attributeName, TValue candidate)
        : base(e => {
            var list = e.GetList<TValue>(attributeName);
            return list.Contains(candidate);
        }) {
    }
}