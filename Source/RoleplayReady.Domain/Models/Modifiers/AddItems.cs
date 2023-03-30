namespace RoleplayReady.Domain.Models.Modifiers;

public record AddItems<TValue> : Modifier {
    public AddItems(IHasModifiers target, string attributeName, Func<IHasAttributes, IEnumerable<TValue>> getItemsFrom)
        : base(target, e => {
            var list = e.GetAttribute<HashSet<TValue>>(attributeName)?.Value;
            foreach (var item in getItemsFrom(e))
                list?.Add(item);
            return e;
        }) {
    }
}