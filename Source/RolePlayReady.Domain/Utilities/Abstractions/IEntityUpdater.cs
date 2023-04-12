//namespace RolePlayReady.Utilities.Contracts;

//public interface IEntityUpdater {
//    public interface IMain {
//        ISetter Let(string attribute);
//        IChecks CheckIf(string attribute);
//        IConditional If(string attribute);

//        IActionConnector AddJournalEntry(string section, string title, string text);
//        IActionConnector AddJournalEntry(string section, string text);
//        IActionConnector AddTag(string tag);
//        //IActionConnector AddPowerSource(string name, string description, Action<IMain> build);
//    }

//    public interface IActionConnector {
//        IMain And();
//    }

//    public interface ILogicalConnector {
//        IConditional And();
//        IConditional And(string attribute);
//        IActionConnector Then(Action<IMain> onTrue, Action<IMain>? onFalse = null);
//    }

//    public interface ISetter {
//        IActionConnector Be<TValue>(TValue value);
//        IActionConnector Be<TValue>(Func<IEntity, TValue> getValueFrom);

//        IActionConnector IncreaseBy<TValue>(TValue bonus) where TValue : IAdditionOperators<TValue, TValue, TValue>;
//        IActionConnector IncreaseBy<TValue>(Func<IEntity, TValue> getBonusFrom) where TValue : IAdditionOperators<TValue, TValue, TValue>;
//        IActionConnector DecreaseBy<TValue>(TValue bonus) where TValue : ISubtractionOperators<TValue, TValue, TValue>;
//        IActionConnector DecreaseBy<TValue>(Func<IEntity, TValue> getBonusFrom) where TValue : ISubtractionOperators<TValue, TValue, TValue>;

//        IActionConnector Have<TKey, TValue>(TKey key, TValue value) where TKey : notnull;
//        IActionConnector Have<TKey, TValue>(TKey key, Func<IEntity, TValue> getValueFrom) where TKey : notnull;
//        IActionConnector Have<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> items) where TKey : notnull;
//        IActionConnector Have<TKey, TValue>(Func<IEntity, IEnumerable<KeyValuePair<TKey, TValue>>> getItemsFrom) where TKey : notnull;

//        IActionConnector Have<TValue>(TValue item);
//        IActionConnector Have<TValue>(Func<IEntity, TValue> getItemFrom);
//        IActionConnector Have<TValue>(params TValue[] items);
//        IActionConnector Have<TValue>(Func<IEntity, IEnumerable<TValue>> getItemsFrom);
//    }

//    public interface IChecks {
//        IActionConnector Contains<TValue>(TValue candidate, string message);
//        IActionConnector ContainsKey<TKey>(TKey key, string message) where TKey : notnull;
//        IActionConnector IsEqualTo<TValue>(TValue validValue, string message) where TValue : IEquatable<TValue>;
//        IActionConnector IsBetween<TValue>(TValue minimum, TValue maximum, string message) where TValue : IComparable<TValue>;
//        IActionConnector IsGreaterThan<TValue>(TValue minimum, string message) where TValue : IComparable<TValue>;
//        IActionConnector IsGreaterOrEqualTo<TValue>(TValue minimum, string message) where TValue : IComparable<TValue>;
//        IActionConnector IsLessThan<TValue>(TValue maximum, string message) where TValue : IComparable<TValue>;
//        IActionConnector IsLessOrEqualTo<TValue>(TValue maximum, string message) where TValue : IComparable<TValue>;
//    }

//    public interface IConditional {
//        ILogicalConnector Contains<TValue>(TValue candidate);
//        ILogicalConnector ContainsKey<TKey>(TKey key) where TKey : notnull;
//        ILogicalConnector IsEqualTo<TValue>(TValue validValue) where TValue : IEquatable<TValue>;
//        ILogicalConnector IsBetween<TValue>(TValue minimum, TValue maximum) where TValue : IComparable<TValue>;
//        ILogicalConnector IsGreaterThan<TValue>(TValue minimum) where TValue : IComparable<TValue>;
//        ILogicalConnector IsGreaterOrEqualTo<TValue>(TValue minimum) where TValue : IComparable<TValue>;
//        ILogicalConnector IsLessThan<TValue>(TValue maximum) where TValue : IComparable<TValue>;
//        ILogicalConnector IsLessOrEqualTo<TValue>(TValue maximum) where TValue : IComparable<TValue>;
//    }
//}
