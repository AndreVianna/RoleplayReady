namespace RoleplayReady.Domain.Models.Effects;

public record IsBetween<TValue> : Effect
    where TValue : IComparable<TValue> {
    [SetsRequiredMembers]
    public IsBetween(string attributeName, TValue minimum, TValue maximum, string message)
        : base(e => {
            e.Validations.Add(new Validation(x => {
                var attribute = x.GetAttribute<TValue>(attributeName);
                return attribute is not null && attribute.Value is not null && attribute.Value.CompareTo(minimum) >= 0 &&
                       attribute.Value.CompareTo(maximum) <= 0;
            }, message));
            return e;
        }) {
    }
}