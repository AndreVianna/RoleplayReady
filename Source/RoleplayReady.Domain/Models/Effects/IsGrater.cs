namespace RoleplayReady.Domain.Models.Effects;

public record IsGrater<TValue> : Effect
    where TValue : IComparable<TValue> {
    [SetsRequiredMembers]
    public IsGrater(string attributeName, TValue minimum, string message)
        : base(e => {
            e.Validations.Add(new Validation(x => {
                    var attribute = x.GetAttribute<TValue>(attributeName);
                    return attribute is not null && attribute.Value is not null && attribute.Value.CompareTo(minimum) > 0;
                }, message));
            return e;
        }) {
    }
}