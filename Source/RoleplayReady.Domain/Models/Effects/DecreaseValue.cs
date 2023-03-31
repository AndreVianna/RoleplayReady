namespace RoleplayReady.Domain.Models.Effects;

public record DecreaseValue<TValue> : Effect
    where TValue : ISubtractionOperators<TValue, TValue, TValue> {
    [SetsRequiredMembers]
    public DecreaseValue(string attributeName, Func<IElement, TValue> getBonusFrom)
        : base(e => {
            var attribute = e.GetAttribute<TValue>(attributeName);
            if (attribute is not null && attribute.Value is not null)
                attribute.Value -= getBonusFrom(e);
            return e;
        }) {
    }
}