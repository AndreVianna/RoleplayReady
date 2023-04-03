namespace RolePlayReady.Models.Contracts;

public interface IEntityAttribute : ICloneable {
    IAttribute Attribute { get; init; }
    IEntity Entity { get; set; }
    object Value { get; set; }
    IList<Func<IEntityAttribute, ValidationResult>> Validations { get; }

    bool IsValid { get; }
    ValidationResult Validate();
    IEntityAttribute CloneUnder(IEntity entity);
}

public interface IEntityFlag
    : IEntityAttribute {
    new IAttribute<bool> Attribute { get; init; }
    new bool Value { get; set; }
    new IList<Func<IEntityFlag, ValidationResult>> Validations { get; }
}

public interface IEntityValue<TValue>
    : IEntityAttribute {
    new IAttribute<TValue> Attribute { get; init; }
    new TValue Value { get; set; }
    new IList<Func<IEntityValue<TValue>, ValidationResult>> Validations { get; }
}

public interface IEntityList<TValue>
    : IEntityAttribute {
    new IAttribute<HashSet<TValue>> Attribute { get; init; }
    new HashSet<TValue> Value { get; }
    new IList<Func<IEntityList<TValue>, ValidationResult>> Validations { get; }
}

public interface IEntityMap<TKey, TValue>
    : IEntityAttribute
    where TKey : notnull {
    new IAttribute<Dictionary<TKey, TValue>> Attribute { get; init; }
    new Dictionary<TKey, TValue> Value { get; }
    new IList<Func<IEntityMap<TKey, TValue>, ValidationResult>> Validations { get; }
}