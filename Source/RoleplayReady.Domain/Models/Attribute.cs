namespace RolePlayReady.Models;

public abstract record Attribute<TSelf, TValue> : IAttribute<TSelf, TValue>
    where TSelf : class, IAttribute<TSelf, TValue> {
    protected Attribute() { }

    [SetsRequiredMembers]
    protected Attribute(IAttributeDefinition attributeDefinition, TValue value) {
        AttributeDefinition = Throw.IfNull(attributeDefinition);
        Value = Throw.IfNull(value);
    }

    // Entity + AttributeDefinition must be unique;
    public required IAttributeDefinition AttributeDefinition { get; init; }
    object? IAttribute.Value => Value;
    public TValue Value { get; set; } = default!;
}

public record FlagAttribute : Attribute<IFlagAttribute, bool>, IFlagAttribute {
    public FlagAttribute() { }

    [SetsRequiredMembers]
    public FlagAttribute(IAttributeDefinition attributeDefinition, bool value)
        : base(attributeDefinition, value) {
        Value = Throw.IfNull(value);
    }
}

public record SimpleAttribute<TValue> : Attribute<ISimpleAttribute<TValue>, TValue>, ISimpleAttribute<TValue>
    where TValue : notnull {
    public SimpleAttribute() { }

    [SetsRequiredMembers]
    public SimpleAttribute(IAttributeDefinition attributeDefinition, TValue value)
        : base(attributeDefinition, value) {
        Value = Throw.IfNull(value);
    }
}

public record SetAttribute<TValue> : Attribute<ISetAttribute<TValue>, HashSet<TValue>>, ISetAttribute<TValue> {
    public SetAttribute() { }

    [SetsRequiredMembers]
    public SetAttribute(IAttributeDefinition attributeDefinition, HashSet<TValue> value)
        : base(attributeDefinition, value) {
        Value = Throw.IfNull(value);
    }
}

public record ListAttribute<TValue> : Attribute<IListAttribute<TValue>, List<TValue>>, IListAttribute<TValue> {
    public ListAttribute() { }

    [SetsRequiredMembers]
    public ListAttribute(IAttributeDefinition attributeDefinition, List<TValue> value)
        : base(attributeDefinition, value) {
        Value = Throw.IfNull(value);
    }
}

public record MapAttribute<TKey, TValue> : Attribute<IMapAttribute<TKey, TValue>, Dictionary<TKey, TValue>>, IMapAttribute<TKey, TValue>
    where TKey : notnull {
    public MapAttribute() { }

    [SetsRequiredMembers]
    public MapAttribute(IAttributeDefinition attributeDefinition, Dictionary<TKey, TValue> value)
        : base(attributeDefinition, value) {
        Value = Throw.IfNull(value);
    }
}