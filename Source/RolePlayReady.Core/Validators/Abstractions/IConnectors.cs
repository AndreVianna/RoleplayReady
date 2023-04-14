namespace System.Validators.Abstractions;

public interface IConnectors<out TChecks> : IValidatorResult {
    TChecks And { get; }
}

public interface ICollectionConnectors<TItem> : IConnectors<ICollectionChecks<TItem>> { }

public interface IDictionaryConnectors<TKey, TValue> : IConnectors<IDictionaryChecks<TKey, TValue>> { }
public interface IComplexConnectors : IConnectors<IComplexChecks> { }

public interface IStringConnectors : IConnectors<IStringChecks> { }

public interface INumericConnectors : IConnectors<INumericChecks> { }

public interface IDateTimeConnectors : IConnectors<IDateTimeChecks> { }

public interface ITypeConnectors : IConnectors<ITypeChecks> { }
