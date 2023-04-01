namespace RoleplayReady.Domain.Models.Modifiers;

public record IncreaseValueModifier<TValue> : ElementModifier
    where TValue : IAdditionOperators<TValue, TValue, TValue> {
    [SetsRequiredMembers]
    public IncreaseValueModifier(string attributeName, Func<IElement, TValue> getBonusFrom)
        : base(e => {
            var attribute = e.GetAttribute<TValue>(attributeName);
            if (attribute is not null && attribute.Value is not null)
                attribute.Value += getBonusFrom(e);
            return e;
        }) {
    }
}