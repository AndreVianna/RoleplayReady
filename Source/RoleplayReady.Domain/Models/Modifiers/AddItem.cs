namespace RoleplayReady.Domain.Models.Modifiers;

public record AddItem<TValue> : Modifier {
    public AddItem(IHasModifiers target, string attributeName, Func<IHasAttributes, TValue> getItemFrom)
        : base(target, e => {
            var list = e.GetAttribute<HashSet<TValue>>(attributeName)?.Value;
            list?.Add(getItemFrom(e));
            return e;
        }) {
    }
}