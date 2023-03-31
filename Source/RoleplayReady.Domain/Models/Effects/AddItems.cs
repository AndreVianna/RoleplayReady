namespace RoleplayReady.Domain.Models.Effects;

public record AddItems<TValue> : Effect {
    [SetsRequiredMembers]
    public AddItems(string attributeName, Func<IElement, IEnumerable<TValue>> getItemsFrom)
        : base(e => {
            var list = e.GetAttribute<HashSet<TValue>>(attributeName)?.Value;
            foreach (var item in getItemsFrom(e))
                list?.Add(item);
            return e;
        }) {
    }
}