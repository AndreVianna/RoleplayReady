namespace RolePlayReady.Models.Contracts;

public interface IEntityAttribute {
    IAttribute Attribute { get; }
    object? Value { get; }
}

public interface IEntityAttribute<TSelf, TValue>
    : IEntityAttribute
    where TSelf : IEntityAttribute<TSelf, TValue> {
    new TValue Value { get; set; }
}

public interface IEntityFlagAttribute
    : IEntityAttribute<IEntityFlagAttribute, bool> {
}

public interface IEntitySimpleAttribute<TValue>
    : IEntityAttribute<IEntitySimpleAttribute<TValue>, TValue> {
}

public interface IEntitySetAttribute<TValue>
    : IEntityAttribute<IEntitySetAttribute<TValue>, HashSet<TValue>> {
}

public interface IEntityListAttribute<TValue>
    : IEntityAttribute<IEntityListAttribute<TValue>, List<TValue>> {
}

public interface IEntityDictionaryAttribute<TKey, TValue>
    : IEntityAttribute<IEntityDictionaryAttribute<TKey, TValue>, Dictionary<TKey, TValue>>
    where TKey : notnull {
}