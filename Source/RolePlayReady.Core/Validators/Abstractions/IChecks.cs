namespace System.Validators.Abstractions;

public interface IChecks<out TConnectors> {
    TConnectors NotNull();
}

public interface ICollectionChecks<TItem> : IChecks<ICollectionConnectors<TItem>> {
    ICollectionConnectors<TItem> EachItem(Func<TItem, IValidatorResult> validateUsing);
}

public interface IDictionaryChecks<TKey, TValue> : IChecks<IDictionaryConnectors<TKey, TValue>> {
    IDictionaryConnectors<TKey, TValue> EachItem(Func<TValue, IValidatorResult> validateUsing);
}
public interface IComplexChecks : IChecks<IComplexConnectors> {
    IComplexConnectors Valid();
}

public interface IStringChecks : IChecks<IStringConnectors> {
    IStringConnectors NotEmptyOrWhiteSpace();
    IStringConnectors NoLongerThan(int maximumLength);
}

public interface INumericChecks : IChecks<INumericConnectors> {
}

public interface IDateTimeChecks : IChecks<IDateTimeConnectors> {
}

public interface ITypeChecks : IChecks<ITypeConnectors> {
}
