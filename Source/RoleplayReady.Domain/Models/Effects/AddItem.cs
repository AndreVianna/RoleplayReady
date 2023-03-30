namespace RoleplayReady.Domain.Models.Effects;

public record AddItem<TValue> : Effect {
    public AddItem(IHasEffects parent, string attributeName, Func<IHasAttributes, TValue> getItemFrom)
        : base(parent, e => {
            var list = e.GetAttribute<HashSet<TValue>>(attributeName)?.Value;
            list?.Add(getItemFrom(e));
            return e;
        }) {
    }
}