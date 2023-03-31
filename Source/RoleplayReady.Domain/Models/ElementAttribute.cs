namespace RoleplayReady.Domain.Models;

public record ElementAttribute : Child, IElementAttribute {
    public ElementAttribute() {

    }

    [SetsRequiredMembers]
    public ElementAttribute(IEntity parent, string ownerId, string name, IAttribute attribute, object? value, string? description = null, Status? status = null)
        : base(parent, ownerId, name, description, status) {
        Attribute = attribute;
        Value = value;
    }

    public required IAttribute Attribute { get; init; }

    public object? Value { get; set; }
}

public record ElementAttribute<TValue> : ElementAttribute, IElementAttribute<TValue> {
    public ElementAttribute() {

    }

    [SetsRequiredMembers]
    public ElementAttribute(IEntity parent, string ownerId, string name, IAttribute attribute, TValue? value, string? description = null)
        : base(parent, ownerId, name, attribute, value, description) {
    }

    public new required TValue? Value { get; set; }

    object? IHaveValue.Value {
        get => Value;
        set => Value = (TValue?)value;
    }
}