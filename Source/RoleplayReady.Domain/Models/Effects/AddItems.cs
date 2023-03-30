namespace RoleplayReady.Domain.Models.Effects;

public record AddItems<TValue> : Effect {
    public AddItems(IHasEffects parent, string attributeName, Func<IHasAttributes, IEnumerable<TValue>> getItemsFrom)
        : base(parent, e => {
            var list = e.GetAttribute<HashSet<TValue>>(attributeName)?.Value;
            foreach (var item in getItemsFrom(e))
                list?.Add(item);
            return e;
        }) {
    }
}