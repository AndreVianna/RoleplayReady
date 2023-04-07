namespace RolePlayReady.Models;

public abstract record EntityAttribute<TSelf, TValue> : IEntityAttribute<TSelf, TValue>
    where TSelf : class, IEntityAttribute<TSelf, TValue> {
    protected EntityAttribute() { }

    [SetsRequiredMembers]
    protected EntityAttribute(IEntity owner, IAttribute attribute, TValue value) {
        Entity = Throw.IfNull(owner);
        Attribute = Throw.IfNull(attribute);
        Value = Throw.IfNull(value);
    }

    // Entity + Attribute must be unique;
    public required IEntity Entity { get; set; }
    public required IAttribute Attribute { get; init; }
    object? IEntityAttribute.Value => Value;
    public TValue Value { get; set; } = default!;

    public IList<Func<TSelf, ValidationResult>> Validations { get; init; }
        = new List<Func<TSelf, ValidationResult>>();

    public bool IsValid => Validations.All(validate => validate((this as TSelf)!).IsValid);


    public ValidationResult Validate() => Validations
        .Aggregate(ValidationResult.Valid, (current, validate) => current + validate((this as TSelf)!));
    public IEntityAttribute CloneUnder(IEntity entity) => this with { Entity = entity };
}

public record EntityFlag : EntityAttribute<IEntityFlag, bool>, IEntityFlag {
    public EntityFlag() { }

    [SetsRequiredMembers]
    public EntityFlag(IEntity owner, IAttribute attribute, bool value)
        : base(owner, attribute, value) {
        Value = Throw.IfNull(value);
    }
}

public record EntityValue<TValue> : EntityAttribute<IEntityValue<TValue>, TValue>, IEntityValue<TValue>
    where TValue : notnull {
    public EntityValue() { }

    [SetsRequiredMembers]
    public EntityValue(IEntity owner, IAttribute attribute, TValue value)
        : base(owner, attribute, value) {
        Value = Throw.IfNull(value);
    }
}

public record EntityList<TValue> : EntityAttribute<IEntityList<TValue>, HashSet<TValue>>, IEntityList<TValue> {
    public EntityList() { }

    [SetsRequiredMembers]
    public EntityList(IEntity owner, IAttribute attribute, HashSet<TValue> value)
        : base(owner, attribute, value) {
        Value = Throw.IfNull(value);
    }
}

public record EntityMap<TKey, TValue> : EntityAttribute<IEntityMap<TKey, TValue>, Dictionary<TKey, TValue>>, IEntityMap<TKey, TValue>
    where TKey : notnull {
    public EntityMap() { }

    [SetsRequiredMembers]
    public EntityMap(IEntity owner, IAttribute attribute, Dictionary<TKey, TValue> value)
        : base(owner, attribute, value) {
        Value = Throw.IfNull(value);
    }
}