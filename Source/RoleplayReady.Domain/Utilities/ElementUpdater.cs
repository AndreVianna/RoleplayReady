namespace RoleplayReady.Domain.Utilities;

internal class ElementUpdater : IElementUpdater {
    private readonly ICollection<ValidationError> _errors = new List<ValidationError>();

    private ElementUpdater(IElement target, string attribute) {
        Target = target;
        Attribute = attribute;
    }

    public IElement Target { get; }
    public string Attribute { get; }

    public static IElementUpdater.IMain For(IElement target)
        => new Main(target);

    private class Main : ElementUpdater, IElementUpdater.IMain {
        public Main(IElement target) : base(target, null!) {
        }

        public IElementUpdater.ISetter Let(string attribute) {
            if (string.IsNullOrWhiteSpace(attribute))
                throw new ArgumentException($"{nameof(attribute)} cannot be null or white spaces.", nameof(attribute));
            if (!Target.Exists(attribute))
                throw new ArgumentException($"{nameof(attribute)} not found in {Target.Name}.", nameof(attribute));
            return new Setter(Target, attribute);
        }

        public IElementUpdater.IValidator CheckIf(string attribute) {
            if (string.IsNullOrWhiteSpace(attribute))
                throw new ArgumentException($"{nameof(attribute)} cannot be null or white spaces.", nameof(attribute));
            if (!Target.Exists(attribute))
                throw new ArgumentException($"{nameof(attribute)} not found in {Target.Name}.", nameof(attribute));
            return new Validator(Target, attribute);
        }

        public IElementUpdater.IConditional If(string attribute) {
            if (string.IsNullOrWhiteSpace(attribute))
                throw new ArgumentException($"{nameof(attribute)} cannot be null or white spaces.", nameof(attribute));
            if (!Target.Exists(attribute))
                throw new ArgumentException($"{nameof(attribute)} not found in {Target.Name}.", nameof(attribute));
            return new Conditional(Target, attribute);
        }

        public IElementUpdater.IActionConnector AddJournalEntry(EntrySection section, string title, string text) {
            ((Actor)Target).JournalEntries.Add(new JournalEntry(Target, section, title, text));
            return new ActionConnector(Target);
        }

        public IElementUpdater.IActionConnector AddJournalEntry(EntrySection section, string text) {
            ((Actor)Target).JournalEntries.Add(new JournalEntry(Target, section, Attribute, text));
            return new ActionConnector(Target);
        }

        public IElementUpdater.IActionConnector AddTag(string text) {
            Target.Tags.Add(text);
            return new ActionConnector(Target);
        }

        public IElementUpdater.IActionConnector AddPowerSource(string name, string description, Action<IElementUpdater.IMain> build)
            => AddPowerSource(name, description, (_, b) => build(b));

        public IElementUpdater.IActionConnector AddPowerSource(string name, string description, Action<IEntity, IElementUpdater.IMain> build) {
            Target.RuleSet!.Configure(nameof(RuleSet.PowerSources)).As(ps => ps.Add(name, description, build));
            return new ActionConnector(Target);
        }
    }

    private class ActionConnector : ElementUpdater, IElementUpdater.IActionConnector {
        public ActionConnector(IElement element)
            : base(element, null!) {
        }

        public IElementUpdater.IMain And() => new Main(Target);
    }

    private class Setter : ElementUpdater, IElementUpdater.ISetter {
        public Setter(IElement target, string attribute)
            : base(target, attribute) {
        }

        public IElementUpdater.IActionConnector Be<TValue>(TValue value)
            => Be(_ => value);

        public IElementUpdater.IActionConnector Be<TValue>(Func<IElement, TValue> getValueFrom) {
            var modifier = new SetValueModifier<TValue>(Attribute, getValueFrom);
            return new ActionConnector(modifier.Modify(Target));
        }

        public IElementUpdater.IActionConnector Have<TKey, TValue>(TKey key, TValue value)
            where TKey : notnull
            => Have(key, _ => value);

        public IElementUpdater.IActionConnector Have<TKey, TValue>(TKey key, Func<IElement, TValue> getValueFrom)
            where TKey : notnull {
            var modifier = new AddOrSetPairModifier<TKey, TValue>(Attribute, key, getValueFrom);
            return new ActionConnector(modifier.Modify(Target));
        }

        public IElementUpdater.IActionConnector Have<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> items)
            where TKey : notnull
            => Have(_ => items);

        public IElementUpdater.IActionConnector Have<TKey, TValue>(Func<IElement, IEnumerable<KeyValuePair<TKey, TValue>>> getItemsFrom)
            where TKey : notnull {
            var modifier = new AddOrSetPairsModifier<TKey, TValue>(Attribute, getItemsFrom);
            return new ActionConnector(modifier.Modify(Target));
        }

        public IElementUpdater.IActionConnector Have<TValue>(TValue item)
            => Have(_ => item);

        public IElementUpdater.IActionConnector Have<TValue>(Func<IElement, TValue> getItemFrom) {
            var modifier = new AddItemModifier<TValue>(Attribute, getItemFrom);
            return new ActionConnector(modifier.Modify(Target));
        }

        public IElementUpdater.IActionConnector Have<TValue>(params TValue[] items)
            => Have(_ => items);

        public IElementUpdater.IActionConnector Have<TValue>(Func<IElement, IEnumerable<TValue>> getItemsFrom) {
            var modifier = new AddItemsModifier<TValue>(Attribute, getItemsFrom);
            return new ActionConnector(modifier.Modify(Target));
        }

        public IElementUpdater.IActionConnector IncreaseBy<TValue>(TValue bonus)
            where TValue : IAdditionOperators<TValue, TValue, TValue>
            => IncreaseBy(_ => bonus);

        public IElementUpdater.IActionConnector IncreaseBy<TValue>(Func<IElement, TValue> getBonusFrom)
            where TValue : IAdditionOperators<TValue, TValue, TValue> {
            var modifier = new IncreaseValueModifier<TValue>(Attribute, getBonusFrom);
            return new ActionConnector(modifier.Modify(Target));
        }

        public IElementUpdater.IActionConnector DecreaseBy<TValue>(TValue bonus)
            where TValue : ISubtractionOperators<TValue, TValue, TValue>
            => DecreaseBy(_ => bonus);

        public IElementUpdater.IActionConnector DecreaseBy<TValue>(Func<IElement, TValue> getBonusFrom)
            where TValue : ISubtractionOperators<TValue, TValue, TValue> {
            var modifier = new DecreaseValueModifier<TValue>(Attribute, getBonusFrom);
            return new ActionConnector(modifier.Modify(Target));
        }
    }

    private class Validator : ElementUpdater, IElementUpdater.IValidator {
        public Validator(IElement target, string attribute)
            : base(target, attribute) {
        }

        public IElementUpdater.IActionConnector Contains<TValue>(TValue candidate, string message) {
            var validator = new ContainsValidator<TValue>(Attribute, candidate, message);
            return new ActionConnector(validator.Validate(Target, _errors));
        }

        public IElementUpdater.IActionConnector ContainsKey<TKey, TValue>(TKey key, string message)
            where TKey : notnull {
            var validator = new ContainsKeyValidator<TKey, TValue>(Attribute, key, message);
            return new ActionConnector(validator.Validate(Target, _errors));
        }

        public IElementUpdater.IActionConnector IsEqualTo<TValue>(TValue validValue, string message)
            where TValue : IEquatable<TValue> {
            var validator = new IsEqualValidator<TValue>(Attribute, validValue, message);
            return new ActionConnector(validator.Validate(Target, _errors));
        }

        public IElementUpdater.IActionConnector IsBetween<TValue>(TValue minimum, TValue maximum, string message)
            where TValue : IComparable<TValue> {
            var validator = new IsBetweenValidator<TValue>(Attribute, minimum, maximum, message);
            return new ActionConnector(validator.Validate(Target, _errors));
        }

        public IElementUpdater.IActionConnector IsGreaterThan<TValue>(TValue minimum, string message)
            where TValue : IComparable<TValue> {
            var validator = new IsGraterValidator<TValue>(Attribute, minimum, message);
            return new ActionConnector(validator.Validate(Target, _errors));
        }

        public IElementUpdater.IActionConnector IsGreaterOrEqualTo<TValue>(TValue minimum, string message)
            where TValue : IComparable<TValue> {
            var validator = new IsGraterOrEqualValidator<TValue>(Attribute, minimum, message);
            return new ActionConnector(validator.Validate(Target, _errors));
        }

        public IElementUpdater.IActionConnector IsLessThan<TValue>(TValue maximum, string message)
            where TValue : IComparable<TValue> {
            var validator = new IsLessValidator<TValue>(Attribute, maximum, message);
            return new ActionConnector(validator.Validate(Target, _errors));
        }

        public IElementUpdater.IActionConnector IsLessOrEqualTo<TValue>(TValue maximum, string message)
            where TValue : IComparable<TValue> {
            var validator = new IsLessOrEqualValidator<TValue>(Attribute, maximum, message);
            return new ActionConnector(validator.Validate(Target, _errors));
        }
    }

    private class Conditional : ElementUpdater, IElementUpdater.IConditional {
        private readonly bool _previous;

        public Conditional(IElement target, string attribute, bool previous = true)
            : base(target, attribute) {
            _previous = previous;
        }

        public IElementUpdater.ILogicalConnector Contains<TValue>(TValue candidate) {
            var condition = new ContainsChecker<TValue>(Attribute, candidate);
            return new LogicalConnector(Target, _previous && condition.IsTrueFor(Target));
        }

        public IElementUpdater.ILogicalConnector ContainsKey<TKey, TValue>(TKey key)
            where TKey : notnull {
            var condition = new ContainsKeyChecker<TKey, TValue>(Attribute, key);
            return new LogicalConnector(Target, _previous && condition.IsTrueFor(Target));
        }

        public IElementUpdater.ILogicalConnector IsEqualTo<TValue>(TValue validValue)
            where TValue : IEquatable<TValue> {
            var condition = new IsEqualChecker<TValue>(Attribute, validValue);
            return new LogicalConnector(Target, _previous && condition.IsTrueFor(Target));
        }

        public IElementUpdater.ILogicalConnector IsBetween<TValue>(TValue minimum, TValue maximum)
            where TValue : IComparable<TValue> {
            var condition = new IsBetweenChecker<TValue>(Attribute, minimum, maximum);
            return new LogicalConnector(Target, _previous && condition.IsTrueFor(Target));
        }

        public IElementUpdater.ILogicalConnector IsGreaterThan<TValue>(TValue minimum)
            where TValue : IComparable<TValue> {
            var condition = new IsGraterChecker<TValue>(Attribute, minimum);
            return new LogicalConnector(Target, _previous && condition.IsTrueFor(Target));
        }

        public IElementUpdater.ILogicalConnector IsGreaterOrEqualTo<TValue>(TValue minimum)
            where TValue : IComparable<TValue> {
            var condition = new IsGraterOrEqualChecker<TValue>(Attribute, minimum);
            return new LogicalConnector(Target, _previous && condition.IsTrueFor(Target));
        }

        public IElementUpdater.ILogicalConnector IsLessThan<TValue>(TValue maximum)
            where TValue : IComparable<TValue> {
            var condition = new IsLessChecker<TValue>(Attribute, maximum);
            return new LogicalConnector(Target, _previous && condition.IsTrueFor(Target));
        }

        public IElementUpdater.ILogicalConnector IsLessOrEqualTo<TValue>(TValue maximum)
            where TValue : IComparable<TValue> {
            var condition = new IsLessOrEqualChecker<TValue>(Attribute, maximum);
            return new LogicalConnector(Target, _previous && condition.IsTrueFor(Target));
        }
    }

    private class LogicalConnector : ElementUpdater, IElementUpdater.ILogicalConnector {
        private readonly bool _result;

        public LogicalConnector(IElement target, bool result)
            : base(target, null!) {
            _result = result;
        }

        public IElementUpdater.IConditional And()
            => new Conditional(Target, Attribute, _result);

        public IElementUpdater.IConditional And(string attribute)
            => new Conditional(Target, attribute, _result);

        public IElementUpdater.IActionConnector Then(Action<IElementUpdater.IMain> onTrue, Action<IElementUpdater.IMain>? onFalse = null) {
            var newBranch = new Main(Target);
            if (_result)
                onTrue(newBranch);
            else
                onFalse?.Invoke(newBranch);
            return new ActionConnector(Target);
        }
    }
}
