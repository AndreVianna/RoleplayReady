namespace RoleplayReady.Domain.Operations.Changes;

public record DecreaseValue<TValue> : EntityChange
    where TValue : ISubtractionOperators<TValue, TValue, TValue> {
    [SetsRequiredMembers]
    public DecreaseValue(string attributeName, Func<IEntity, TValue> getBonusFrom)
        : base(e => {
            var attribute = e.GetAttribute(attributeName);
            if (attribute.Value is TValue)
                attribute.Value -= getBonusFrom(e);
            return e;
        }) {
    }
}