namespace RolePlayReady.Models.Contracts;

public interface IAttribute {
    IAttributeDefinition AttributeDefinition { get; }
    object? Value { get; }
}

public interface IAttribute<TSelf, TValue>
    : IAttribute
    where TSelf : IAttribute<TSelf, TValue> {
    new TValue Value { get; set; }
}

public interface IFlagAttribute
    : IAttribute<IFlagAttribute, bool> {
}

public interface ISimpleAttribute<TValue>
    : IAttribute<ISimpleAttribute<TValue>, TValue> {
}

public interface ISetAttribute<TValue>
    : IAttribute<ISetAttribute<TValue>, HashSet<TValue>> {
}

public interface IListAttribute<TValue>
    : IAttribute<IListAttribute<TValue>, List<TValue>> {
}

public interface IMapAttribute<TKey, TValue>
    : IAttribute<IMapAttribute<TKey, TValue>, Dictionary<TKey, TValue>>
    where TKey : notnull {
}