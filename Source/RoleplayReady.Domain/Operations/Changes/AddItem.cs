namespace RoleplayReady.Domain.Operations.Changes;

public record AddItem<TValue> : EntityChange {
    [SetsRequiredMembers]
    public AddItem(string attributeName, Func<IEntity, TValue> getItemFrom)
        : base(e => {
            var list = e.GetList<TValue>(attributeName);
            list.Add(getItemFrom(e));
            return e;
        }) {
    }
}