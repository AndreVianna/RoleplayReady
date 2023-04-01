namespace RoleplayReady.Domain.Models.Modifiers;

public record DecreaseValueModifier<TValue> : ElementModifier
    where TValue : ISubtractionOperators<TValue, TValue, TValue> {
    [SetsRequiredMembers]
    public DecreaseValueModifier(string attributeName, Func<IElement, TValue> getBonusFrom)
        : base(e => {
            var attribute = e.GetAttribute<TValue>(attributeName);
            if (attribute is not null && attribute.Value is not null)
                attribute.Value -= getBonusFrom(e);
            return e;
        }) {
    }
}