namespace RoleplayReady.Domain.Models.Modifiers;

public record IfThenElse<TValue> : ElementModifier
    where TValue : IComparable<TValue> {
    [SetsRequiredMembers]
    public IfThenElse(string attributeName, TValue maximum, string message)
        : base(e => {
            e.Validations.Add(new Validation(x => {
                var attribute = x.GetAttribute<TValue>(attributeName);
                return attribute is not null && attribute.Value is not null && attribute.Value.CompareTo(maximum) <= 0;
            }, message));
            return e;
        }) {
    }
}