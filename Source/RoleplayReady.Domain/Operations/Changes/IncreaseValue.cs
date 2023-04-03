using RolePlayReady.Models.Contracts;
using RolePlayReady.Utilities;

namespace RolePlayReady.Operations.Changes;

public record IncreaseValue<TValue> : EntityChange
    where TValue : IAdditionOperators<TValue, TValue, TValue> {
    [SetsRequiredMembers]
    public IncreaseValue(string attributeName, Func<IEntity, TValue> getBonusFrom)
        : base(e => {
            var attribute = e.GetAttribute(attributeName);
            if (attribute.Value is TValue)
                attribute.Value += getBonusFrom(e);
            return e;
        }) {
    }
}