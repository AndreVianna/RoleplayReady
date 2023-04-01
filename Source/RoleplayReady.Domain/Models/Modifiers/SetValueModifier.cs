namespace RoleplayReady.Domain.Models.Modifiers;

public record SetValueModifier<TValue> : ElementModifier {
    [SetsRequiredMembers]
    public SetValueModifier(string attributeName, Func<IElement, TValue> getValueFrom)
        : base(e => {
            var attribute = e.GetAttribute<TValue>(attributeName);
            if (attribute is not null && attribute.Value is not null)
                attribute.Value = getValueFrom(e);
            return e;
        }) {
    }
}