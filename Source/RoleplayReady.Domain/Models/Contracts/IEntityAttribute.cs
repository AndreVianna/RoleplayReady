namespace RoleplayReady.Domain.Models.Contracts;

public interface IEntityAttribute
    : IHaveValue {
    IEntity Entity { get; init; }
    IAttribute Attribute { get; init; }

    IList<Func<IEntityAttribute, ValidationResult>> Validations { get; init; }

    bool IsValid { get; }
    ValidationResult Validate();
    IEntityAttribute CloneUnder(IEntity entity);
}

public interface IEntityFlag
    : IEntityAttribute,
      IHaveFlag {
    new IList<Func<IEntityFlag, ValidationResult>> Validations { get; init; }
}

public interface IEntityValue<TValue>
    : IEntityAttribute,
        IHaveValue<TValue> {
    new IList<Func<IEntityValue<TValue>, ValidationResult>> Validations { get; init; }
}

public interface IEntityList<TValue>
    : IEntityAttribute,
        IHaveList<TValue> {
    new IList<Func<IEntityList<TValue>, ValidationResult>> Validations { get; init; }
}

public interface IEntityMap<TKey, TValue>
    : IEntityAttribute,
        IHaveMap<TKey, TValue>
    where TKey : notnull {
    new IList<Func<IEntityMap<TKey, TValue>, ValidationResult>> Validations { get; init; }
}