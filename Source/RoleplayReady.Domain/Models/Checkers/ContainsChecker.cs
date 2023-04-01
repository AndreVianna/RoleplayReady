namespace RoleplayReady.Domain.Models.Checkers;

public record ContainsChecker<TValue> : ElementChecker {
    [SetsRequiredMembers]
    public ContainsChecker(string attributeName, TValue candidate)
        : base(e => {
            var attribute = e.GetAttribute<HashSet<TValue>>(attributeName);
            return attribute.Value == null || !attribute.Value.Contains(candidate);
        }) {
    }
}