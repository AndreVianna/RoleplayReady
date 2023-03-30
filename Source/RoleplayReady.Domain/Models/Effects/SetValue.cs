namespace RoleplayReady.Domain.Models.Effects;

public record SetValue<TValue> : Effect {
    public SetValue(IHasEffects parent, string attributeName, Func<IHasAttributes, TValue> getValueFrom)
        : base(parent, e => {
            var attribute = e.GetAttribute<TValue>(attributeName);
            if (attribute is not null && attribute.Value is not null)
                attribute.Value = getValueFrom(e);
            return e;
        }) {
    }
}