namespace RoleplayReady.Domain.Models.Modifiers;

public record DecreaseValue<TValue> : Modifier
    where TValue : ISubtractionOperators<TValue, TValue, TValue> {
    public DecreaseValue(IHasModifiers target, string attributeName, Func<IHasAttributes, TValue> getBonusFrom)
        : base(target, e => {
            var attribute = e.GetAttribute<TValue>(attributeName);
            if (attribute is not null && attribute.Value is not null)
                attribute.Value -= getBonusFrom(e);
            return e;
        }) {
    }
}