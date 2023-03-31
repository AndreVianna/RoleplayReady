namespace RoleplayReady.Domain.Models.Effects;

public record SetValue<TValue> : Effect {
    [SetsRequiredMembers]
    public SetValue(string attributeName, Func<IElement, TValue> getValueFrom)
        : base(e => {
            var attribute = e.GetAttribute<TValue>(attributeName);
            if (attribute is not null && attribute.Value is not null)
                attribute.Value = getValueFrom(e);
            return e;
        }) {
    }
}