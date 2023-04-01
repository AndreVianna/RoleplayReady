namespace RoleplayReady.Domain.Models.Modifiers;

public record AddItemModifier<TValue> : ElementModifier {
    [SetsRequiredMembers]
    public AddItemModifier(string attributeName, Func<IElement, TValue> getItemFrom)
        : base(e => {
            var list = e.GetAttribute<HashSet<TValue>>(attributeName)?.Value;
            list?.Add(getItemFrom(e));
            return e;
        }) {
    }
}