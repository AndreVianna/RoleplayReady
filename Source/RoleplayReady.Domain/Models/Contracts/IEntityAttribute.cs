namespace RolePlayReady.Models.Contracts;

public interface IEntityAttribute {
    IEntity Entity { get; set; }
    IAttribute Attribute { get; }
    object? Value { get; }
}

public interface IEntityAttribute<TSelf, TValue>
    : IEntityAttribute
    where TSelf : IEntityAttribute<TSelf, TValue> {
    new IAttribute<TValue> Attribute { get; init; }
    new TValue Value { get; set; }
    IList<Func<TSelf, ValidationResult>> Validations { get; }

    bool IsValid { get; }
    ValidationResult Validate();
}

public interface IEntityFlag
    : IEntityAttribute<IEntityFlag, bool> {
}

public interface IEntityValue<TValue>
    : IEntityAttribute<IEntityValue<TValue>, TValue> {
}

public interface IEntityList<TValue> : IEntityAttribute<IEntityList<TValue>, HashSet<TValue>> {
}

public interface IEntityMap<TKey, TValue> : IEntityAttribute<IEntityMap<TKey, TValue>, Dictionary<TKey, TValue>>
    where TKey : notnull {
}