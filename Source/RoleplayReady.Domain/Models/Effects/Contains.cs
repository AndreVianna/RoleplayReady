namespace RoleplayReady.Domain.Models.Effects;

public record Contains<TValue> : Effect {
    [SetsRequiredMembers]
    public Contains(string attributeName, TValue candidate, string message)
        : base(e => {
            e.Validations.Add(new Validation(x => {
                    var attribute = x.GetAttribute<HashSet<TValue>>(attributeName);
                    return attribute?.Value != null && attribute.Value.Contains(candidate);
                },
                message));
            return e;
        }) {
    }
}