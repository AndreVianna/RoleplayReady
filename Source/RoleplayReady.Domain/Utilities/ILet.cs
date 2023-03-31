namespace RoleplayReady.Domain.Utilities;

public interface ILet {
    IConnectBuilders Be<TValue>(TValue value);
    IConnectBuilders Be<TValue>(Func<IElement, TValue> getValueFrom);

    IConnectBuilders IncreaseBy<TValue>(TValue bonus) where TValue : IAdditionOperators<TValue, TValue, TValue>;
    IConnectBuilders IncreaseBy<TValue>(Func<IElement, TValue> getBonusFrom) where TValue : IAdditionOperators<TValue, TValue, TValue>;
    IConnectBuilders DecreaseBy<TValue>(TValue bonus) where TValue : ISubtractionOperators<TValue, TValue, TValue>;
    IConnectBuilders DecreaseBy<TValue>(Func<IElement, TValue> getBonusFrom) where TValue : ISubtractionOperators<TValue, TValue, TValue>;

    IConnectBuilders Have<TKey, TValue>(TKey key, TValue value) where TKey : notnull;
    IConnectBuilders Have<TKey, TValue>(TKey key, Func<IElement, TValue> getValueFrom) where TKey : notnull;
    IConnectBuilders Have<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> items) where TKey : notnull;
    IConnectBuilders Have<TKey, TValue>(Func<IElement, IEnumerable<KeyValuePair<TKey, TValue>>> getItemsFrom) where TKey : notnull;

    IConnectBuilders Have<TValue>(TValue item);
    IConnectBuilders Have<TValue>(Func<IElement, TValue> getItemFrom);
    IConnectBuilders Have<TValue>(params TValue[] items);
    IConnectBuilders Have<TValue>(Func<IElement, IEnumerable<TValue>> getItemsFrom);
}