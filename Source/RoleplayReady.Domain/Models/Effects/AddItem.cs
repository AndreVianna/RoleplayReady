namespace RoleplayReady.Domain.Models.Effects;

public record AddItem<TValue> : Effect {
    [SetsRequiredMembers]
    public AddItem(string attributeName, Func<IElement, TValue> getItemFrom)
        : base(e => {
            var list = e.GetAttribute<HashSet<TValue>>(attributeName)?.Value;
            list?.Add(getItemFrom(e));
            return e;
        }) {
    }
}