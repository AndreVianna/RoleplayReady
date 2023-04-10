namespace RolePlayReady.Models;

public abstract record EntityAttribute : IEntityAttribute {
    public required IAttribute Attribute { get; init; }
    public object? Value { get; init; }
}

public abstract record EntityAttribute<TSelf, TValue>
    : EntityAttribute,
      IEntityAttribute<TSelf, TValue>
    where TSelf : class, IEntityAttribute<TSelf, TValue> {
    object? IEntityAttribute.Value => Value;
    public new TValue Value { get; set; } = default!;
}

public record EntityFlagAttribute
    : EntityAttribute<IEntityFlagAttribute, bool>,
        IEntityFlagAttribute;

public record EntitySimpleAttribute<TValue>
    : EntityAttribute<IEntitySimpleAttribute<TValue>, TValue>,
        IEntitySimpleAttribute<TValue>
    where TValue : notnull;

public record EntitySetAttribute<TValue>
    : EntityAttribute<IEntitySetAttribute<TValue>, HashSet<TValue>>,
        IEntitySetAttribute<TValue>;

public record EntityListAttribute<TValue>
    : EntityAttribute<IEntityListAttribute<TValue>, List<TValue>>,
        IEntityListAttribute<TValue>;

public record EntityDictionaryAttribute<TKey, TValue>
    : EntityAttribute<IEntityDictionaryAttribute<TKey, TValue>, Dictionary<TKey, TValue>>,
        IEntityDictionaryAttribute<TKey, TValue>
    where TKey : notnull;