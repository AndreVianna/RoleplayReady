namespace RoleplayReady.Domain.Models;

public record ElementFeature<TValue> : Feature<TValue>, IElementFeature, IHasValue<TValue>
{
    public TValue? Value { get; set; }

    object? IHasValue.Value
    {
        get => Value;
        set => Value = (TValue?)value;
    }
}
