namespace RoleplayReady.Domain.Models.Effects;

public record IncreaseValue<TValue> : Effect
    where TValue : IAdditionOperators<TValue, TValue, TValue> {
    [SetsRequiredMembers]
    public IncreaseValue(string attributeName, Func<IElement, TValue> getBonusFrom)
        : base(e => {
            var attribute = e.GetAttribute<TValue>(attributeName);
            if (attribute is not null && attribute.Value is not null)
                attribute.Value += getBonusFrom(e);
            return e;
        }) {
    }
}