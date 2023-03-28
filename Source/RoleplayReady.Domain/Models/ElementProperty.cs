namespace RoleplayReady.Domain.Models;

public record ElementProperty<TValue> : Property<TValue>, IElementProperty, IHasValue<TValue>
{
    public TValue? Value { get; set; }

    object? IHasValue.Value
    {
        get => Value;
        set => Value = (TValue?)value;
    }
}
