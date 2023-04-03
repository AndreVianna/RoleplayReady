using RolePlayReady.Models.Contracts;
using RolePlayReady.Utilities;

namespace RolePlayReady.Operations.Changes;

public record AddOrSetPair<TKey, TValue> : EntityChange
    where TKey : notnull {
    [SetsRequiredMembers]
    public AddOrSetPair(string attributeName, TKey key, Func<IEntity, TValue> getValueFrom)
        : base(e => {
            var dictionary = e.GetMap<TKey, TValue>(attributeName);
            if (dictionary is not null)
                dictionary[key] = getValueFrom(e);

            return e;
        }) {
    }
}