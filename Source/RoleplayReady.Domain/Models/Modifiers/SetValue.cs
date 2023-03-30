namespace RoleplayReady.Domain.Models.Modifiers;

public record SetValue<TValue> : Modifier {
    public SetValue(IHasModifiers target, string attributeName, Func<IHasAttributes, TValue> getValueFrom)
        : base(target, e => {
            var attribute = e.GetAttribute<TValue>(attributeName);
            if (attribute is not null && attribute.Value is not null)
                attribute.Value = getValueFrom(e);
            return e;
        }) {
    }
}