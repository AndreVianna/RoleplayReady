namespace RoleplayReady.Domain.Models.Checkers;

public record ContainsKeyChecker<TKey, TValue> : ElementChecker
    where TKey : notnull {
    [SetsRequiredMembers]
    public ContainsKeyChecker(string attributeName, TKey key)
        : base(e => {
            var attribute = e.GetAttribute<Dictionary<TKey, TValue>>(attributeName);
            return attribute?.Value != null && attribute.Value.ContainsKey(key);
        }) {
    }
}