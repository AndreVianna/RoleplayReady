namespace RoleplayReady.Domain.Models.Effects;

public record Contains<TValue> : Effect {
    public Contains(IHasEffects parent, string attributeName, TValue candidate, string message, Severity severity = Suggestion)
        : base(parent, e => {
            e.Validations.Add(new() {
                Parent = e,
                Severity = severity,
                Validate = x => {
                    var attribute = x.GetAttribute<HashSet<TValue>>(attributeName);
                    return attribute?.Value != null && attribute.Value.Contains(candidate);
                },
                Message = message,
            });
            return e;
        }) {
    }
}