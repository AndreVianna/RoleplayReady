namespace RoleplayReady.Domain.Models;

public record AttributeWithValue<TValue>() : Attribute(typeof(TValue)), IAttributeWithValue, IHasValue<TValue>
{
    public TValue? Value { get; set; }

    object? IHasValue.Value
    {
        get => Value;
        set => Value = (TValue?)value;
    }
}
