using RolePlayReady.Utilities;

namespace RolePlayReady.Operations.Assertions;

public record ContainsKey<TKey> : EntityAssertion
    where TKey : notnull {
    [SetsRequiredMembers]
    public ContainsKey(string attributeName, TKey key)
        : base(e => {
            var value = e.GetValue(attributeName);
            return value is IDictionary dictionary && dictionary.Contains(key);
        }) {
    }
}