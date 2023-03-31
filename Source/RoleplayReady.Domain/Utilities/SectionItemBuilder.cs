namespace RoleplayReady.Domain.Utilities;

public class SectionItemBuilder : IFluentBuilder, IConnectBuilders, ILet, ICheck {
    private readonly IElement _element;
    private string _attribute = string.Empty;

    private SectionItemBuilder(IElement element) {
        _element = element;
    }

    public static IFluentBuilder For(IElement target) => new SectionItemBuilder(target);

    public ILet Let(string attribute) {
        _attribute = attribute;
        return this;
    }

    public ICheck CheckIf(string attribute) {
        _attribute = attribute;
        return this;
    }

    public IFluentBuilder And => this;

    public IConnectBuilders Be<TValue>(TValue value)
        => Be(_ => value);

    public IConnectBuilders Be<TValue>(Func<IElement, TValue> getValueFrom)
        => Add(new SetValue<TValue>(_attribute, getValueFrom));

    public IConnectBuilders Have<TKey, TValue>(TKey key, TValue value)
        where TKey : notnull
        => Have(key, _ => value);

    public IConnectBuilders Have<TKey, TValue>(TKey key, Func<IElement, TValue> getValueFrom)
        where TKey : notnull
        => Add(new AddOrSetPair<TKey, TValue>(_attribute, key, getValueFrom));

    public IConnectBuilders Have<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> items)
        where TKey : notnull
        => Have(_ => items);

    public IConnectBuilders Have<TKey, TValue>(Func<IElement, IEnumerable<KeyValuePair<TKey, TValue>>> getItemsFrom)
        where TKey : notnull
        => Add(new AddOrSetPairs<TKey, TValue>(_attribute, getItemsFrom));

    public IConnectBuilders Have<TValue>(TValue item)
        => Have(_ => item);

    public IConnectBuilders Have<TValue>(Func<IElement, TValue> getItemFrom)
        => Add(new AddItem<TValue>(_attribute, getItemFrom));

    public IConnectBuilders Have<TValue>(params TValue[] items)
        => Have(_ => items);

    public IConnectBuilders Have<TValue>(Func<IElement, IEnumerable<TValue>> getItemsFrom)
        => Add(new AddItems<TValue>(_attribute, getItemsFrom));

    public IConnectBuilders IncreaseBy<TValue>(TValue bonus)
        where TValue : IAdditionOperators<TValue, TValue, TValue>
        => IncreaseBy(_ => bonus);

    public IConnectBuilders IncreaseBy<TValue>(Func<IElement, TValue> getBonusFrom)
        where TValue : IAdditionOperators<TValue, TValue, TValue>
        => Add(new IncreaseValue<TValue>(_attribute, getBonusFrom));

    public IConnectBuilders DecreaseBy<TValue>(TValue bonus)
        where TValue : ISubtractionOperators<TValue, TValue, TValue>
        => DecreaseBy(_ => bonus);

    public IConnectBuilders DecreaseBy<TValue>(Func<IElement, TValue> getBonusFrom)
        where TValue : ISubtractionOperators<TValue, TValue, TValue>
        => Add(new DecreaseValue<TValue>(_attribute, getBonusFrom));

    public IConnectBuilders Contains<TValue>(TValue candidate, string message, Severity severity = Suggestion)
        => Add(new Contains<TValue>(_attribute, candidate, message));

    public IConnectBuilders ContainsKey<TKey, TValue>(TKey key, string message, Severity severity = Suggestion)
        where TKey : notnull
        => Add(new ContainsKey<TKey, TValue>(_attribute, key, message));

    public IConnectBuilders IsEqualTo<TValue>(TValue validValue, string message, Severity severity = Suggestion)
        where TValue : IEquatable<TValue>
        => Add(new IsEqual<TValue>(_attribute, validValue, message));

    public IConnectBuilders IsBetween<TValue>(TValue minimum, TValue maximum, string message, Severity severity = Suggestion)
        where TValue : IComparable<TValue>
        => Add(new IsBetween<TValue>(_attribute, minimum, maximum, message));

    public IConnectBuilders IsGreaterThan<TValue>(TValue minimum, string message, Severity severity = Suggestion)
        where TValue : IComparable<TValue>
        => Add(new IsGrater<TValue>(_attribute, minimum, message));

    public IConnectBuilders IsGreaterOrEqualTo<TValue>(TValue minimum, string message, Severity severity = Suggestion)
        where TValue : IComparable<TValue>
        => Add(new IsGraterOrEqual<TValue>(_attribute, minimum, message));

    public IConnectBuilders IsLessThan<TValue>(TValue maximum, string message, Severity severity = Suggestion)
        where TValue : IComparable<TValue>
        => Add(new IsLess<TValue>(_attribute, maximum, message));

    public IConnectBuilders IsLessOrEqualTo<TValue>(TValue maximum, string message, Severity severity = Suggestion)
        where TValue : IComparable<TValue>
        => Add(new IsLessOrEqual<TValue>(_attribute, maximum, message));

    public IConnectBuilders AddJournalEntry(EntrySection section, string title, string text)
        => Add(new AddJournalEntry(section, title, text));

    public IConnectBuilders AddJournalEntry(EntrySection section, string text)
        => Add(new AddJournalEntry(section, _attribute, text));

    public IConnectBuilders AddTag(string text)
        => Add(new AddTag(text));

    public IConnectBuilders AddPowerSource(string name, string description, Action<IFluentBuilder> build)
        => AddPowerSource(name, description, (_, b) => build(b));

    public IConnectBuilders AddPowerSource(string name, string description, Action<IElement, IFluentBuilder> build)
        => Add(new AddPowerSource(name, description, build));

    private IConnectBuilders Add(IEffects effect) {
        _element.Effects.Add(effect);
        return this;
    }
}
