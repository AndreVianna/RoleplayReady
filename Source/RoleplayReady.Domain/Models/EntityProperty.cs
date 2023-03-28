namespace RoleplayReady.Domain.Models;

public interface IEntityProperty : IHasValue
{
    Property Property { get; init; }
}

public class EntityProperty<TValue> : IEntityProperty, IHasValue<TValue>
{
    public required Property Property { get; init; }

    public TValue? Value { get; set; }

    object? IHasValue.Value
    {
        get => Value;
        set => Value = (TValue?)value;
    }
};
