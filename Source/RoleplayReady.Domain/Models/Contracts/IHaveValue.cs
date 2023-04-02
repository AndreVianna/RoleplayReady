namespace RoleplayReady.Domain.Models.Contracts;

public interface IHaveValue {
    object Value { get; init; }
}

public interface IHaveFlag
    : IHaveValue {
    new bool Value { get; init; }
}

public interface IHaveValue<TValue>
    : IHaveValue {
    new TValue Value { get; init; }
}

public interface IHaveList<TValue>
    : IHaveValue {
    new HashSet<TValue> Value { get; init; }
}

public interface IHaveMap<TKey, TValue>
    : IHaveValue
    where TKey : notnull {
    new Dictionary<TKey, TValue> Value { get; init; }
}

