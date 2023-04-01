namespace RoleplayReady.Domain.Models.Modifiers;

public record AddItemsModifier<TValue> : ElementModifier {
    [SetsRequiredMembers]
    public AddItemsModifier(string attributeName, Func<IElement, IEnumerable<TValue>> getItemsFrom)
        : base(e => {
            var list = e.GetAttribute<HashSet<TValue>>(attributeName)?.Value;
            foreach (var item in getItemsFrom(e))
                list?.Add(item);
            return e;
        }) {
    }
}