namespace RoleplayReady.Domain.Operations.Changes;

public record SetValue<TValue> : EntityChange {
    [SetsRequiredMembers]
    public SetValue(string attributeName, Func<IEntity, TValue> getValueFrom)
        : base(e => {
            var attribute = e.GetAttribute(attributeName);
            if (attribute.Value is TValue)
                attribute.Value = getValueFrom(e);
            return e;
        }) {
    }
}