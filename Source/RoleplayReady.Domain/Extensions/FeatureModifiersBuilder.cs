namespace RoleplayReady.Domain.Extensions;

internal interface IModifierBuilder {
    ILet Let(string featureName);
    ICheck CheckIf(string featureName);
    IChainModifiers AddJournalEntry(EntryType type, string title, string text);
    IChainModifiers AddJournalEntry(string text);
}

internal interface IChainModifiers {
    IModifierBuilder And { get; }
}

internal interface ILet {
    IChainModifiers Be<TValue>(TValue value);
    IChainModifiers Be<TValue>(Func<IHasAttributes, TValue> getValueFrom);
    
    IChainModifiers IncreaseBy<TValue>(TValue bonus) where TValue : IAdditionOperators<TValue, TValue, TValue>;
    IChainModifiers IncreaseBy<TValue>(Func<IHasAttributes, TValue> getBonusFrom) where TValue : IAdditionOperators<TValue, TValue, TValue>;
    IChainModifiers DecreaseBy<TValue>(TValue bonus) where TValue : ISubtractionOperators<TValue, TValue, TValue>;
    IChainModifiers DecreaseBy<TValue>(Func<IHasAttributes, TValue> getBonusFrom) where TValue : ISubtractionOperators<TValue, TValue, TValue>;

    IChainModifiers Have<TKey, TValue>(TKey key, TValue value) where TKey : notnull;
    IChainModifiers Have<TKey, TValue>(TKey key, Func<IHasAttributes, TValue> getValueFrom) where TKey : notnull;
    IChainModifiers Have<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> items) where TKey : notnull;
    IChainModifiers Have<TKey, TValue>(Func<IHasAttributes, IEnumerable<KeyValuePair<TKey, TValue>>> getItemsFrom) where TKey : notnull;

    IChainModifiers Have<TValue>(TValue item);
    IChainModifiers Have<TValue>(Func<IHasAttributes, TValue> getItemFrom);
    IChainModifiers Have<TValue>(params TValue[] items);
    IChainModifiers Have<TValue>(Func<IHasAttributes, IEnumerable<TValue>> getItemsFrom);
}

internal interface ICheck {
    IChainModifiers Contains<TValue>(TValue candidate, string message, ValidationSeverityLevel severityLevel = Hint);
    IChainModifiers ContainsKey<TKey, TValue>(TKey key, string message, ValidationSeverityLevel severityLevel = Hint)
        where TKey : notnull;
    IChainModifiers IsEqualTo<TValue>(TValue validValue, string message, ValidationSeverityLevel severityLevel = Hint)
        where TValue : IEquatable<TValue>;
    IChainModifiers IsBetween<TValue>(TValue minimum, TValue maximum, string message, ValidationSeverityLevel severityLevel = Hint)
        where TValue : IComparable<TValue>;
    IChainModifiers IsGreaterThan<TValue>(TValue minimum, string message, ValidationSeverityLevel severityLevel = Hint)
        where TValue : IComparable<TValue>;
    IChainModifiers IsGreaterOrEqualTo<TValue>(TValue minimum, string message, ValidationSeverityLevel severityLevel = Hint)
        where TValue : IComparable<TValue>;
    IChainModifiers IsLessThan<TValue>(TValue maximum, string message, ValidationSeverityLevel severityLevel = Hint)
        where TValue : IComparable<TValue>;
    IChainModifiers IsLessOrEqualTo<TValue>(TValue maximum, string message, ValidationSeverityLevel severityLevel = Hint)
        where TValue : IComparable<TValue>;
}

internal class FeatureModifiersBuilder : IModifierBuilder, IChainModifiers, ILet, ICheck {
    private readonly IHasModifiers _target;
    private string _feature = string.Empty;

    private FeatureModifiersBuilder(IHasModifiers target) {
        _target = target;
    }

    public static IModifierBuilder For(IHasModifiers target) => new FeatureModifiersBuilder(target);

    public ILet Let(string featureName) {
        _feature = featureName;
        return this;
    }

    public ICheck CheckIf(string featureName) {
        _feature = featureName;
        return this;
    }

    public ILet AndLet(string featureName) {
        _feature = featureName;
        return this;
    }

    public ICheck AndCheckIf(string featureName) {
        _feature = featureName;
        return this;
    }

    public IChainModifiers Be<TValue>(TValue value)
        => Be( _ => value);

    public IChainModifiers Be<TValue>(Func<IHasAttributes, TValue> getValueFrom)
        => Add(new SetValue<TValue>(_target, _feature, getValueFrom));

    public IChainModifiers Have<TKey, TValue>(TKey key, TValue value)
        where TKey : notnull
        => Have(key, _ => value);

    public IChainModifiers Have<TKey, TValue>(TKey key, Func<IHasAttributes, TValue> getValueFrom)
        where TKey : notnull
        => Add(new AddOrSetPair<TKey, TValue>(_target, _feature, key, getValueFrom));

    public IChainModifiers Have<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> items)
        where TKey : notnull
        => Have(_ => items);

    public IChainModifiers Have<TKey, TValue>(Func<IHasAttributes, IEnumerable<KeyValuePair<TKey, TValue>>> getItemsFrom)
        where TKey : notnull
        => Add(new AddOrSetPairs<TKey, TValue>(_target, _feature, getItemsFrom));

    public IChainModifiers Have<TValue>(TValue item)
        => Have(_ => item);

    public IChainModifiers Have<TValue>(Func<IHasAttributes, TValue> getItemFrom)
        => Add(new AddItem<TValue>(_target, _feature, getItemFrom));

    public IChainModifiers Have<TValue>(params TValue[] items)
        => Have(_ => items);

    public IChainModifiers Have<TValue>(Func<IHasAttributes, IEnumerable<TValue>> getItemsFrom)
        => Add(new AddItems<TValue>(_target, _feature, getItemsFrom));

    public IChainModifiers IncreaseBy<TValue>(TValue bonus)
        where TValue : IAdditionOperators<TValue, TValue, TValue>
        => IncreaseBy(_ => bonus);

    public IChainModifiers IncreaseBy<TValue>(Func<IHasAttributes, TValue> getBonusFrom)
        where TValue : IAdditionOperators<TValue, TValue, TValue>
        => Add(new IncreaseValue<TValue>(_target, _feature, getBonusFrom));

    public IChainModifiers DecreaseBy<TValue>(TValue bonus)
        where TValue : ISubtractionOperators<TValue, TValue, TValue>
        => DecreaseBy(_ => bonus);

    public IChainModifiers DecreaseBy<TValue>(Func<IHasAttributes, TValue> getBonusFrom)
        where TValue : ISubtractionOperators<TValue, TValue, TValue>
        => Add(new DecreaseValue<TValue>(_target, _feature, getBonusFrom));

    public IChainModifiers Contains<TValue>(TValue candidate, string message, ValidationSeverityLevel severityLevel = Hint)
        => Add(new Contains<TValue>(_target, _feature, candidate, message, severityLevel));

    public IChainModifiers ContainsKey<TKey, TValue>(TKey key, string message, ValidationSeverityLevel severityLevel = Hint)
        where TKey : notnull
        => Add(new ContainsKey<TKey, TValue>(_target, _feature, key, message, severityLevel));

    public IChainModifiers IsEqualTo<TValue>(TValue validValue, string message, ValidationSeverityLevel severityLevel = Hint)
        where TValue : IEquatable<TValue>
        => Add(new IsEqual<TValue>(_target, _feature, validValue, message, severityLevel));

    public IChainModifiers IsBetween<TValue>(TValue minimum, TValue maximum, string message, ValidationSeverityLevel severityLevel = Hint)
        where TValue : IComparable<TValue>
        => Add(new IsBetween<TValue>(_target, _feature, minimum, maximum, message, severityLevel));

    public IChainModifiers IsGreaterThan<TValue>(TValue minimum, string message, ValidationSeverityLevel severityLevel = Hint)
        where TValue : IComparable<TValue>
        => Add(new IsGrater<TValue>(_target, _feature, minimum, message, severityLevel));

    public IChainModifiers IsGreaterOrEqualTo<TValue>(TValue minimum, string message, ValidationSeverityLevel severityLevel = Hint)
        where TValue : IComparable<TValue>
        => Add(new IsGraterOrEqual<TValue>(_target, _feature, minimum, message, severityLevel));

    public IChainModifiers IsLessThan<TValue>(TValue maximum, string message, ValidationSeverityLevel severityLevel = Hint)
        where TValue : IComparable<TValue>
        => Add(new IsLess<TValue>(_target, _feature, maximum, message, severityLevel));

    public IChainModifiers IsLessOrEqualTo<TValue>(TValue maximum, string message, ValidationSeverityLevel severityLevel = Hint)
        where TValue : IComparable<TValue>
        => Add(new IsLessOrEqual<TValue>(_target, _feature, maximum, message, severityLevel));

    public IChainModifiers AddJournalEntry(EntryType type, string title, string text)
        => Add(new AddJournalEntry(_target, type, title, text));

    public IChainModifiers AddJournalEntry(string text)
        => Add(new AddJournalEntry(_target, EntryType.Feature, _feature, text));

    public IModifierBuilder And => this;

    private FeatureModifiersBuilder Add(Modifier modifier) {
        _target.Modifiers.Add(modifier);
        return this;
    }
}