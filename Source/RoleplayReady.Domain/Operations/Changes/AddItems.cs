using RolePlayReady.Utilities;

namespace RolePlayReady.Operations.Changes;

public record AddItems<TValue> : EntityChange {
    [SetsRequiredMembers]
    public AddItems(string attributeName, Func<IEntity, IEnumerable<TValue>> getItemsFrom)
        : base(e => {
            var list = e.GetList<TValue>(attributeName);
            foreach (var item in getItemsFrom(e))
                list.Add(item);
            return e;
        }) {
    }
}