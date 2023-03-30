namespace RoleplayReady.Domain.Models.Modifiers;

public record IncreaseValue<TValue> : Modifier
    where TValue : IAdditionOperators<TValue, TValue, TValue> {
    public IncreaseValue(IHasModifiers target, string attributeName, Func<IHasAttributes, TValue> getBonusFrom)
        : base(target, e => {
            var attribute = e.GetAttribute<TValue>(attributeName);
            if (attribute is not null && attribute.Value is not null)
                attribute.Value += getBonusFrom(e);
            return e;
        }) {
    }
}