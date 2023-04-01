namespace RoleplayReady.Domain.Models;

public abstract record ElementAttribute : IElementAttribute {
    protected ElementAttribute() { }

    [SetsRequiredMembers]
    protected ElementAttribute(IEntity parent, IElement element, IAttribute attribute, object? value) {
        Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        Element = element ?? throw new ArgumentNullException(nameof(element));
        Attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
        Value = value;
    }

    // Element + Attribute must be unique;
    public required IEntity Parent { get; init; }
    public required IElement Element { get; init; }
    public required IAttribute Attribute { get; init; }

    public object? Value { get; set; }
}

public record ElementAttribute<TValue> : ElementAttribute, IElementAttribute<TValue> {
    public ElementAttribute() {

    }

    [SetsRequiredMembers]
    public ElementAttribute(IEntity parent, IElement element, IAttribute attribute, TValue? value)
        : base(parent, element, attribute, value) {
    }

    public new required TValue? Value { get; set; }

    object? IHaveValue.Value {
        get => Value;
        set => Value = (TValue?)value;
    }
}