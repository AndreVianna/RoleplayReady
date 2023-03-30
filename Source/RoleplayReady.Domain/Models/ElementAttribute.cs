namespace RoleplayReady.Domain.Models;

public record ElementAttribute<TValue>() : Attribute(typeof(TValue)), IElementAttribute, IHasValue<TValue>
{
    public TValue? Value { get; set; }

    object? IHasValue.Value
    {
        get => Value;
        set => Value = (TValue?)value;
    }
}
