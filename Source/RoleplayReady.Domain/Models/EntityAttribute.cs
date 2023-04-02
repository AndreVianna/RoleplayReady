namespace RoleplayReady.Domain.Models;

public abstract record EntityAttribute
    : IEntityAttribute {
    protected EntityAttribute() { }

    [SetsRequiredMembers]
    protected EntityAttribute(IEntity owner, IAttribute attribute, object value) {
        Entity = Throw.IfNull(owner);
        Attribute = Throw.IfNull(attribute);
        Value = Throw.IfNull(value);
    }

    // Entity + Attribute must be unique;
    public required IEntity Entity { get; init; }
    public required IAttribute Attribute { get; init; }
    public object Value { get; init; } = default!;

    public IList<Func<IEntityAttribute, ValidationResult>> Validations { get; init; }
        = new List<Func<IEntityAttribute, ValidationResult>>();

    public bool IsValid => Validations.All(validate => validate(this).IsValid);
    public ValidationResult Validate() => Validations
        .Aggregate(ValidationResult.Valid,
            (current, validate) =>
                current + validate(this));
    public IEntityAttribute CloneUnder(IEntity entity) => this with { Entity = entity };
}

public record EntityFlag
    : EntityAttribute,
        IEntityFlag {
    public EntityFlag() { }

    [SetsRequiredMembers]
    public EntityFlag(IEntity owner, IAttribute attribute, bool value)
        : base(owner, attribute, value) {
        Value = Throw.IfNull(value);
    }

    object IHaveValue.Value {
        get => Value;
        init => Value = (bool)(value);
    }

    public new required bool Value { get; init; }


    IList<Func<IEntityAttribute, ValidationResult>> IEntityAttribute.Validations {
        get => Validations
            .Select(i => (Func<IEntityAttribute, ValidationResult>)(_ => i(this)))
            .ToList();
        init => Validations = value
            .Select(i => (Func<IEntityFlag, ValidationResult>)(_ => i(this)))
            .ToList();

    }

    public new IList<Func<IEntityFlag, ValidationResult>> Validations { get; init; }
        = new List<Func<IEntityFlag, ValidationResult>>();
}

public record EntityValue<TValue>
    : EntityAttribute,
      IEntityValue<TValue>
    where TValue : notnull {
    public EntityValue() { }

    [SetsRequiredMembers]
    public EntityValue(IEntity owner, IAttribute attribute, TValue value)
        : base(owner, attribute, value) {
        Value = Throw.IfNull(value);
    }

    public new required TValue Value { get; init; }

    object IHaveValue.Value {
        get => Value;
        init => Value = (TValue)Throw.IfNull(value);
    }

    IList<Func<IEntityAttribute, ValidationResult>> IEntityAttribute.Validations {
        get => Validations
            .Select(i => (Func<IEntityAttribute, ValidationResult>)(_ => i(this)))
            .ToList();
        init => Validations = value
            .Select(i => (Func<IEntityValue<TValue>, ValidationResult>)(_ => i(this)))
            .ToList();

    }

    public new IList<Func<IEntityValue<TValue>, ValidationResult>> Validations { get; init; }
        = new List<Func<IEntityValue<TValue>, ValidationResult>>();
}

public record EntityList<TValue>
    : EntityAttribute,
      IEntityList<TValue> {
    public EntityList() { }

    [SetsRequiredMembers]
    public EntityList(IEntity owner, IAttribute attribute, HashSet<TValue> value)
        : base(owner, attribute, value) {
        Value = Throw.IfNull(value);
    }

    public new required HashSet<TValue> Value { get; init; }

    object IHaveValue.Value {
        get => Value;
        init => Value = Throw.IfNull(value as HashSet<TValue>);
    }

    IList<Func<IEntityAttribute, ValidationResult>> IEntityAttribute.Validations {
        get => Validations
            .Select(i => (Func<IEntityAttribute, ValidationResult>)(_ => i(this)))
            .ToList();
        init => Validations = value
            .Select(i => (Func<IEntityList<TValue>, ValidationResult>)(_ => i(this)))
            .ToList();

    }

    public new IList<Func<IEntityList<TValue>, ValidationResult>> Validations { get; init; }
        = new List<Func<IEntityList<TValue>, ValidationResult>>();
}

public record EntityMap<TKey, TValue>
    : EntityAttribute,
      IEntityMap<TKey, TValue>
    where TKey : notnull {
    public EntityMap() { }

    [SetsRequiredMembers]
    public EntityMap(IEntity owner, IAttribute attribute, Dictionary<TKey, TValue> value)
        : base(owner, attribute, value) {
        Value = Throw.IfNull(value);
    }

    public new required Dictionary<TKey, TValue> Value { get; init; }

    object IHaveValue.Value {
        get => Value;
        init => Value = Throw.IfNull(value as Dictionary<TKey, TValue>);
    }

    IList<Func<IEntityAttribute, ValidationResult>> IEntityAttribute.Validations {
        get => Validations
            .Select(i => (Func<IEntityAttribute, ValidationResult>)(_ => i(this)))
            .ToList();
        init => Validations = value
            .Select(i => (Func<IEntityMap<TKey, TValue>, ValidationResult>)(_ => i(this)))
            .ToList();

    }

    public new IList<Func<IEntityMap<TKey, TValue>, ValidationResult>> Validations { get; init; }
        = new List<Func<IEntityMap<TKey, TValue>, ValidationResult>>();
}