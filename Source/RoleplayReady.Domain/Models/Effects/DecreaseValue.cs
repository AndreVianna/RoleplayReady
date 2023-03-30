namespace RoleplayReady.Domain.Models.Effects;

public record DecreaseValue<TValue> : Effect
    where TValue : ISubtractionOperators<TValue, TValue, TValue> {
    public DecreaseValue(IHasEffects parent, string attributeName, Func<IHasAttributes, TValue> getBonusFrom)
        : base(parent, e => {
            var attribute = e.GetAttribute<TValue>(attributeName);
            if (attribute is not null && attribute.Value is not null)
                attribute.Value -= getBonusFrom(e);
            return e;
        }) {
    }
}