using RoleplayReady.Domain.Models.Effects;

namespace RoleplayReady.Domain.Utilities;

internal class ElementUpdater : IElementUpdater {
    private readonly IElementUpdater _previous = null!;

    private ElementUpdater(IElement target) {
        Target = target;
    }

    protected ElementUpdater(IElement target, IElementUpdater previous) {
        Target = target;
        _previous = previous;
    }

    public IElement Target { get; }

    public static IElementUpdater.IElementUpdaterMain For(IElement target) => new ElementUpdaterMain(target, new ElementUpdater(target));

    private IElementUpdater.IElementUpdaterMainConnector Add(IEffects effect) {
        Target.Effects.Add(effect);
        return new ElementUpdaterMainConnector(Target, this);
    }

    private class ElementUpdaterMain : ElementUpdater, IElementUpdater.IElementUpdaterMain {
        public ElementUpdaterMain(IElement target, IElementUpdater previous) : base(target, previous) {
        }

        public string Attribute { get; private set; } = string.Empty;

        public IElementUpdater.IElementUpdaterSetter Let(string attribute) {
            Attribute = attribute;
            return new ElementUpdaterSetter(Target, this);
        }

        public IElementUpdater.IElementUpdaterValidation Validate(string attribute) {
            Attribute = attribute;
            return new ElementUpdaterValidation(Target, this);
        }

        public IElementUpdater.IElementUpdaterConditional If(string attribute) {
            Attribute = attribute;
            return new ElementUpdaterConditional(Target, this);
        }

        public IElementUpdater.IElementUpdaterMainConnector AddJournalEntry(EntrySection section, string title, string text)
            => Add(new AddJournalEntry(section, title, text));

        public IElementUpdater.IElementUpdaterMainConnector AddJournalEntry(EntrySection section, string text)
            => Add(new AddJournalEntry(section, Attribute, text));

        public IElementUpdater.IElementUpdaterMainConnector AddTag(string text)
            => Add(new AddTag(text));

        public IElementUpdater.IElementUpdaterMainConnector AddPowerSource(string name, string description, Action<IElementUpdater.IElementUpdaterMain> build)
            => AddPowerSource(name, description, (_, b) => build(b));

        public IElementUpdater.IElementUpdaterMainConnector AddPowerSource(string name, string description, Action<IElement, IElementUpdater.IElementUpdaterMain> build)
            => Add(new AddPowerSource(name, description, build));
    }

    private class ElementUpdaterMainConnector : ElementUpdater, IElementUpdater.IElementUpdaterMainConnector {
        public ElementUpdaterMainConnector(IElement element, IElementUpdater previous) : base(element, previous) { }

        public IElementUpdater.IElementUpdaterMain And() => new ElementUpdaterMain(Target, this);
    }

    private class ElementUpdaterSetter : ElementUpdater, IElementUpdater.IElementUpdaterSetter {
        public ElementUpdaterSetter(IElement target, IElementUpdater previous) : base(target, previous) {
        }

        public IElementUpdater.IElementUpdaterMainConnector Be<TValue>(TValue value)
            => Be(_ => value);

        public IElementUpdater.IElementUpdaterMainConnector Be<TValue>(Func<IElement, TValue> getValueFrom)
            => Add(new SetValue<TValue>(((ElementUpdaterMain)_previous).Attribute, getValueFrom));

        public IElementUpdater.IElementUpdaterMainConnector Have<TKey, TValue>(TKey key, TValue value)
            where TKey : notnull
            => Have(key, _ => value);

        public IElementUpdater.IElementUpdaterMainConnector Have<TKey, TValue>(TKey key, Func<IElement, TValue> getValueFrom)
            where TKey : notnull
            => Add(new AddOrSetPair<TKey, TValue>(((ElementUpdaterMain)_previous).Attribute, key, getValueFrom));

        public IElementUpdater.IElementUpdaterMainConnector Have<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> items)
            where TKey : notnull
            => Have(_ => items);

        public IElementUpdater.IElementUpdaterMainConnector Have<TKey, TValue>(Func<IElement, IEnumerable<KeyValuePair<TKey, TValue>>> getItemsFrom)
            where TKey : notnull
            => Add(new AddOrSetPairs<TKey, TValue>(((ElementUpdaterMain)_previous).Attribute, getItemsFrom));

        public IElementUpdater.IElementUpdaterMainConnector Have<TValue>(TValue item)
            => Have(_ => item);

        public IElementUpdater.IElementUpdaterMainConnector Have<TValue>(Func<IElement, TValue> getItemFrom)
            => Add(new AddItem<TValue>(((ElementUpdaterMain)_previous).Attribute, getItemFrom));

        public IElementUpdater.IElementUpdaterMainConnector Have<TValue>(params TValue[] items)
            => Have(_ => items);

        public IElementUpdater.IElementUpdaterMainConnector Have<TValue>(Func<IElement, IEnumerable<TValue>> getItemsFrom)
            => Add(new AddItems<TValue>(((ElementUpdaterMain)_previous).Attribute, getItemsFrom));

        public IElementUpdater.IElementUpdaterMainConnector IncreaseBy<TValue>(TValue bonus)
            where TValue : IAdditionOperators<TValue, TValue, TValue>
            => IncreaseBy(_ => bonus);

        public IElementUpdater.IElementUpdaterMainConnector IncreaseBy<TValue>(Func<IElement, TValue> getBonusFrom)
            where TValue : IAdditionOperators<TValue, TValue, TValue>
            => Add(new IncreaseValue<TValue>(((ElementUpdaterMain)_previous).Attribute, getBonusFrom));

        public IElementUpdater.IElementUpdaterMainConnector DecreaseBy<TValue>(TValue bonus)
            where TValue : ISubtractionOperators<TValue, TValue, TValue>
            => DecreaseBy(_ => bonus);

        public IElementUpdater.IElementUpdaterMainConnector DecreaseBy<TValue>(Func<IElement, TValue> getBonusFrom)
            where TValue : ISubtractionOperators<TValue, TValue, TValue>
            => Add(new DecreaseValue<TValue>(((ElementUpdaterMain)_previous).Attribute, getBonusFrom));
    }

    private class ElementUpdaterValidation : ElementUpdater, IElementUpdater.IElementUpdaterValidation {
        public ElementUpdaterValidation(IElement target, IElementUpdater previous) : base(target, previous) {
        }

        public IElementUpdater.IElementUpdaterMainConnector Contains<TValue>(TValue candidate, string message, Severity severity = Suggestion)
            => Add(new Contains<TValue>(((ElementUpdaterMain)_previous).Attribute, candidate, message));

        public IElementUpdater.IElementUpdaterMainConnector ContainsKey<TKey, TValue>(TKey key, string message, Severity severity = Suggestion)
            where TKey : notnull
            => Add(new ContainsKey<TKey, TValue>(((ElementUpdaterMain)_previous).Attribute, key, message));

        public IElementUpdater.IElementUpdaterMainConnector IsEqualTo<TValue>(TValue validValue, string message, Severity severity = Suggestion)
            where TValue : IEquatable<TValue>
            => Add(new IsEqual<TValue>(((ElementUpdaterMain)_previous).Attribute, validValue, message));

        public IElementUpdater.IElementUpdaterMainConnector IsBetween<TValue>(TValue minimum, TValue maximum, string message, Severity severity = Suggestion)
            where TValue : IComparable<TValue>
            => Add(new IsBetween<TValue>(((ElementUpdaterMain)_previous).Attribute, minimum, maximum, message));

        public IElementUpdater.IElementUpdaterMainConnector IsGreaterThan<TValue>(TValue minimum, string message, Severity severity = Suggestion)
            where TValue : IComparable<TValue>
            => Add(new IsGrater<TValue>(((ElementUpdaterMain)_previous).Attribute, minimum, message));

        public IElementUpdater.IElementUpdaterMainConnector IsGreaterOrEqualTo<TValue>(TValue minimum, string message, Severity severity = Suggestion)
            where TValue : IComparable<TValue>
            => Add(new IsGraterOrEqual<TValue>(((ElementUpdaterMain)_previous).Attribute, minimum, message));

        public IElementUpdater.IElementUpdaterMainConnector IsLessThan<TValue>(TValue maximum, string message, Severity severity = Suggestion)
            where TValue : IComparable<TValue>
            => Add(new IsLess<TValue>(((ElementUpdaterMain)_previous).Attribute, maximum, message));

        public IElementUpdater.IElementUpdaterMainConnector IsLessOrEqualTo<TValue>(TValue maximum, string message, Severity severity = Suggestion)
            where TValue : IComparable<TValue>
            => Add(new IsLessOrEqual<TValue>(((ElementUpdaterMain)_previous).Attribute, maximum, message));
    }

    private class ElementUpdaterConditional : ElementUpdater, IElementUpdater.IElementUpdaterConditional {
        public ElementUpdaterConditional(IElement target, IElementUpdater previous) : base(target, previous) {
        }

        public IElementUpdater.IElementUpdaterConditionalConnector Contains<TValue>(TValue candidate, string message, Severity severity = Suggestion)
            => Evaluate(new Contains<TValue>(((ElementUpdaterMain)_previous).Attribute, candidate, message));

        public IElementUpdater.IElementUpdaterConditionalConnector ContainsKey<TKey, TValue>(TKey key, string message, Severity severity = Suggestion)
            where TKey : notnull
            => Evaluate(new ContainsKey<TKey, TValue>(((ElementUpdaterMain)_previous).Attribute, key, message));

        public IElementUpdater.IElementUpdaterConditionalConnector IsEqualTo<TValue>(TValue validValue, string message, Severity severity = Suggestion)
            where TValue : IEquatable<TValue>
            => Evaluate(new IsEqual<TValue>(((ElementUpdaterMain)_previous).Attribute, validValue, message));

        public IElementUpdater.IElementUpdaterConditionalConnector IsBetween<TValue>(TValue minimum, TValue maximum, string message, Severity severity = Suggestion)
            where TValue : IComparable<TValue>
            => Evaluate(new IsBetween<TValue>(((ElementUpdaterMain)_previous).Attribute, minimum, maximum, message));

        public IElementUpdater.IElementUpdaterConditionalConnector IsGreaterThan<TValue>(TValue minimum, string message, Severity severity = Suggestion)
            where TValue : IComparable<TValue>
            => Evaluate(new IsGrater<TValue>(((ElementUpdaterMain)_previous).Attribute, minimum, message));

        public IElementUpdater.IElementUpdaterConditionalConnector IsGreaterOrEqualTo<TValue>(TValue minimum, string message, Severity severity = Suggestion)
            where TValue : IComparable<TValue>
            => Evaluate(new IsGraterOrEqual<TValue>(((ElementUpdaterMain)_previous).Attribute, minimum, message));

        public IElementUpdater.IElementUpdaterConditionalConnector IsLessThan<TValue>(TValue maximum, string message, Severity severity = Suggestion)
            where TValue : IComparable<TValue>
            => Evaluate(new IsLess<TValue>(((ElementUpdaterMain)_previous).Attribute, maximum, message));

        public IElementUpdater.IElementUpdaterConditionalConnector IsLessOrEqualTo<TValue>(TValue maximum, string message, Severity severity = Suggestion)
            where TValue : IComparable<TValue>
            => Evaluate(new IsLessOrEqual<TValue>(((ElementUpdaterMain)_previous).Attribute, maximum, message));
    }

    private class ElementUpdaterConditionalConnector : ElementUpdater, IElementUpdater.IElementUpdaterConditionalConnector {
        public ElementUpdaterConditionalConnector(IElement target, IElementUpdater previous) : base(target, previous) {
        }

        public IElementUpdater.IElementUpdaterConditional And() => throw new NotImplementedException();

        public IElementUpdater.IElementUpdaterConditional And(string attribute) => throw new NotImplementedException();

        public IElementUpdater.IElementUpdaterMain Then(Action<IElementUpdater.IElementUpdaterMain> ifTrue, Action<IElementUpdater.IElementUpdaterMain>? ifFalse = null) {
            Add(new IfThenElse<TValue>())
        }
    }
}
