using RolePlayReady.Utilities;

namespace RolePlayReady.Operations.Changes;

public record AddOrSetPairs<TKey, TValue> : EntityChange
    where TKey : notnull {
    [SetsRequiredMembers]
    public AddOrSetPairs(string attributeName, Func<IEntity, IEnumerable<KeyValuePair<TKey, TValue>>> getItemsFrom)
        : base(e => {
            var dictionary = e.GetMap<TKey, TValue>(attributeName);
            if (dictionary is not null)
                foreach (var item in getItemsFrom(e))
                    dictionary[item.Key] = item.Value;

            return e;
        }) {
    }
}