namespace RoleplayReady.Domain.Utilities;

public interface IElementUpdater {
    public interface IElementUpdaterMain {
        IElementUpdaterSetter Let(string attribute);
        IElementUpdaterValidation Validate(string attribute);
        IElementUpdaterConditional If(string attribute);
        IElementUpdaterMainConnector AddJournalEntry(EntrySection section, string title, string text);
        IElementUpdaterMainConnector AddJournalEntry(EntrySection section, string text);
        IElementUpdaterMainConnector AddTag(string tag);
        IElementUpdaterMainConnector AddPowerSource(string name, string description, Action<IElementUpdaterMain> build);
        //IElementUpdaterActionConnector AddPowerSource(string name, string description, Action<IElement, IElementUpdaterAction> build);
    }

    public interface IElementUpdaterMainConnector {
        IElementUpdaterMain And();
    }

    public interface IElementUpdaterConditionalConnector {
        IElementUpdaterConditional And();
        IElementUpdaterConditional And(string attribute);
        IElementUpdaterMain Then(Action<IElementUpdaterMain> ifTrue, Action<IElementUpdaterMain>? ifFalse = null);
    }

    public interface IElementUpdaterSetter {
        IElementUpdaterMainConnector Be<TValue>(TValue value);
        IElementUpdaterMainConnector Be<TValue>(Func<IElement, TValue> getValueFrom);

        IElementUpdaterMainConnector IncreaseBy<TValue>(TValue bonus) where TValue : IAdditionOperators<TValue, TValue, TValue>;
        IElementUpdaterMainConnector IncreaseBy<TValue>(Func<IElement, TValue> getBonusFrom) where TValue : IAdditionOperators<TValue, TValue, TValue>;
        IElementUpdaterMainConnector DecreaseBy<TValue>(TValue bonus) where TValue : ISubtractionOperators<TValue, TValue, TValue>;
        IElementUpdaterMainConnector DecreaseBy<TValue>(Func<IElement, TValue> getBonusFrom) where TValue : ISubtractionOperators<TValue, TValue, TValue>;

        IElementUpdaterMainConnector Have<TKey, TValue>(TKey key, TValue value) where TKey : notnull;
        IElementUpdaterMainConnector Have<TKey, TValue>(TKey key, Func<IElement, TValue> getValueFrom) where TKey : notnull;
        IElementUpdaterMainConnector Have<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> items) where TKey : notnull;
        IElementUpdaterMainConnector Have<TKey, TValue>(Func<IElement, IEnumerable<KeyValuePair<TKey, TValue>>> getItemsFrom) where TKey : notnull;

        IElementUpdaterMainConnector Have<TValue>(TValue item);
        IElementUpdaterMainConnector Have<TValue>(Func<IElement, TValue> getItemFrom);
        IElementUpdaterMainConnector Have<TValue>(params TValue[] items);
        IElementUpdaterMainConnector Have<TValue>(Func<IElement, IEnumerable<TValue>> getItemsFrom);
    }

    public interface IElementUpdaterValidation {
        IElementUpdaterMainConnector Contains<TValue>(TValue candidate, string message, Severity severity = Suggestion);
        IElementUpdaterMainConnector ContainsKey<TKey, TValue>(TKey key, string message, Severity severity = Suggestion) where TKey : notnull;
        IElementUpdaterMainConnector IsEqualTo<TValue>(TValue validValue, string message, Severity severity = Suggestion) where TValue : IEquatable<TValue>;
        IElementUpdaterMainConnector IsBetween<TValue>(TValue minimum, TValue maximum, string message, Severity severity = Suggestion) where TValue : IComparable<TValue>;
        IElementUpdaterMainConnector IsGreaterThan<TValue>(TValue minimum, string message, Severity severity = Suggestion) where TValue : IComparable<TValue>;
        IElementUpdaterMainConnector IsGreaterOrEqualTo<TValue>(TValue minimum, string message, Severity severity = Suggestion) where TValue : IComparable<TValue>;
        IElementUpdaterMainConnector IsLessThan<TValue>(TValue maximum, string message, Severity severity = Suggestion) where TValue : IComparable<TValue>;
        IElementUpdaterMainConnector IsLessOrEqualTo<TValue>(TValue maximum, string message, Severity severity = Suggestion) where TValue : IComparable<TValue>;
    }

    public interface IElementUpdaterConditional {
        IElementUpdaterConditionalConnector Contains<TValue>(TValue candidate, string message, Severity severity = Suggestion);
        IElementUpdaterConditionalConnector ContainsKey<TKey, TValue>(TKey key, string message, Severity severity = Suggestion) where TKey : notnull;
        IElementUpdaterConditionalConnector IsEqualTo<TValue>(TValue validValue, string message, Severity severity = Suggestion) where TValue : IEquatable<TValue>;
        IElementUpdaterConditionalConnector IsBetween<TValue>(TValue minimum, TValue maximum, string message, Severity severity = Suggestion) where TValue : IComparable<TValue>;
        IElementUpdaterConditionalConnector IsGreaterThan<TValue>(TValue minimum, string message, Severity severity = Suggestion) where TValue : IComparable<TValue>;
        IElementUpdaterConditionalConnector IsGreaterOrEqualTo<TValue>(TValue minimum, string message, Severity severity = Suggestion) where TValue : IComparable<TValue>;
        IElementUpdaterConditionalConnector IsLessThan<TValue>(TValue maximum, string message, Severity severity = Suggestion) where TValue : IComparable<TValue>;
        IElementUpdaterConditionalConnector IsLessOrEqualTo<TValue>(TValue maximum, string message, Severity severity = Suggestion) where TValue : IComparable<TValue>;
    }
}
