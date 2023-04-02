namespace RoleplayReady.Domain.Utilities;

internal class ComponentUpdater : IComponentUpdater {
    private readonly ICollection<ValidationError> _errors = new List<ValidationError>();

    private ComponentUpdater(IComponent target, string attribute) {
        Target = target;
        Attribute = attribute;
    }

    public IComponent Target { get; }
    public string Attribute { get; }

    public static IComponentUpdater.IMain For(IComponent target)
        => new Main(target);

    private class Main : ComponentUpdater, IComponentUpdater.IMain {
        public Main(IComponent target) : base(target, null!) {
        }

        public IComponentUpdater.ISetter Let(string attribute) {
            if (string.IsNullOrWhiteSpace(attribute))
                throw new ArgumentException($"{nameof(attribute)} cannot be null or white spaces.", nameof(attribute));
            if (!Target.Exists(attribute))
                throw new ArgumentException($"{nameof(attribute)} not found in {Target.Name}.", nameof(attribute));
            return new Setter(Target, attribute);
        }

        public IComponentUpdater.IValidator CheckIf(string attribute) {
            if (string.IsNullOrWhiteSpace(attribute))
                throw new ArgumentException($"{nameof(attribute)} cannot be null or white spaces.", nameof(attribute));
            if (!Target.Exists(attribute))
                throw new ArgumentException($"{nameof(attribute)} not found in {Target.Name}.", nameof(attribute));
            return new Validator(Target, attribute);
        }

        public IComponentUpdater.IConditional If(string attribute) {
            if (string.IsNullOrWhiteSpace(attribute))
                throw new ArgumentException($"{nameof(attribute)} cannot be null or white spaces.", nameof(attribute));
            if (!Target.Exists(attribute))
                throw new ArgumentException($"{nameof(attribute)} not found in {Target.Name}.", nameof(attribute));
            return new Conditional(Target, attribute);
        }

        public IComponentUpdater.IActionConnector AddJournalEntry(EntrySection section, string title, string text) {
            ((Actor)Target).JournalEntries.Add(new JournalEntry(Target, section, title, text));
            return new ActionConnector(Target);
        }

        public IComponentUpdater.IActionConnector AddJournalEntry(EntrySection section, string text) {
            ((Actor)Target).JournalEntries.Add(new JournalEntry(Target, section, Attribute, text));
            return new ActionConnector(Target);
        }

        public IComponentUpdater.IActionConnector AddTag(string text) {
            Target.Tags.Add(text);
            return new ActionConnector(Target);
        }

        public IComponentUpdater.IActionConnector AddPowerSource(string name, string description, Action<IComponentUpdater.IMain> build)
            => AddPowerSource(name, description, (_, b) => build(b));

        public IComponentUpdater.IActionConnector AddPowerSource(string name, string description, Action<IEntity, IComponentUpdater.IMain> build) {
            Target.RuleSet!.Configure(nameof(RuleSet.PowerSources)).As(ps => ps.Add(name, description, build));
            return new ActionConnector(Target);
        }
    }

    private class ActionConnector : ComponentUpdater, IComponentUpdater.IActionConnector {
        public ActionConnector(IComponent element)
            : base(element, null!) {
        }

        public IComponentUpdater.IMain And() => new Main(Target);
    }

    private class Setter : ComponentUpdater, IComponentUpdater.ISetter {
        public Setter(IComponent target, string attribute)
            : base(target, attribute) {
        }

        public IComponentUpdater.IActionConnector Be<TValue>(TValue value)
            => Be(_ => value);

        public IComponentUpdater.IActionConnector Be<TValue>(Func<IComponent, TValue> getValueFrom) {
            var modifier = new SetValue<TValue>(Attribute, getValueFrom);
            return new ActionConnector(modifier.Modify(Target));
        }

        public IComponentUpdater.IActionConnector Have<TKey, TValue>(TKey key, TValue value)
            where TKey : notnull
            => Have(key, _ => value);

        public IComponentUpdater.IActionConnector Have<TKey, TValue>(TKey key, Func<IComponent, TValue> getValueFrom)
            where TKey : notnull {
            var modifier = new AddOrSetPair<TKey, TValue>(Attribute, key, getValueFrom);
            return new ActionConnector(modifier.Modify(Target));
        }

        public IComponentUpdater.IActionConnector Have<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> items)
            where TKey : notnull
            => Have(_ => items);

        public IComponentUpdater.IActionConnector Have<TKey, TValue>(Func<IComponent, IEnumerable<KeyValuePair<TKey, TValue>>> getItemsFrom)
            where TKey : notnull {
            var modifier = new AddOrSetPairs<TKey, TValue>(Attribute, getItemsFrom);
            return new ActionConnector(modifier.Modify(Target));
        }

        public IComponentUpdater.IActionConnector Have<TValue>(TValue item)
            => Have(_ => item);

        public IComponentUpdater.IActionConnector Have<TValue>(Func<IComponent, TValue> getItemFrom) {
            var modifier = new AddItem<TValue>(Attribute, getItemFrom);
            return new ActionConnector(modifier.Modify(Target));
        }

        public IComponentUpdater.IActionConnector Have<TValue>(params TValue[] items)
            => Have(_ => items);

        public IComponentUpdater.IActionConnector Have<TValue>(Func<IComponent, IEnumerable<TValue>> getItemsFrom) {
            var modifier = new AddItems<TValue>(Attribute, getItemsFrom);
            return new ActionConnector(modifier.Modify(Target));
        }

        public IComponentUpdater.IActionConnector IncreaseBy<TValue>(TValue bonus)
            where TValue : IAdditionOperators<TValue, TValue, TValue>
            => IncreaseBy(_ => bonus);

        public IComponentUpdater.IActionConnector IncreaseBy<TValue>(Func<IComponent, TValue> getBonusFrom)
            where TValue : IAdditionOperators<TValue, TValue, TValue> {
            var modifier = new IncreaseValue<TValue>(Attribute, getBonusFrom);
            return new ActionConnector(modifier.Modify(Target));
        }

        public IComponentUpdater.IActionConnector DecreaseBy<TValue>(TValue bonus)
            where TValue : ISubtractionOperators<TValue, TValue, TValue>
            => DecreaseBy(_ => bonus);

        public IComponentUpdater.IActionConnector DecreaseBy<TValue>(Func<IComponent, TValue> getBonusFrom)
            where TValue : ISubtractionOperators<TValue, TValue, TValue> {
            var modifier = new DecreaseValue<TValue>(Attribute, getBonusFrom);
            return new ActionConnector(modifier.Modify(Target));
        }
    }

    private class Validator : ComponentUpdater, IComponentUpdater.IValidator {
        public Validator(IComponent target, string attribute)
            : base(target, attribute) {
        }

        public IComponentUpdater.IActionConnector Contains<TValue>(TValue candidate, string message) {
            var validator = new Operations.Validations.Contains<TValue>(Attribute, candidate, message);
            return new ActionConnector(validator.Validate(Target, _errors));
        }

        public IComponentUpdater.IActionConnector ContainsKey<TKey, TValue>(TKey key, string message)
            where TKey : notnull {
            var validator = new Operations.Validations.ContainsKey<TKey, TValue>(Attribute, key, message);
            return new ActionConnector(validator.Validate(Target, _errors));
        }

        public IComponentUpdater.IActionConnector IsEqualTo<TValue>(TValue validValue, string message)
            where TValue : IEquatable<TValue> {
            var validator = new Operations.Validations.IsEqual<TValue>(Attribute, validValue, message);
            return new ActionConnector(validator.Validate(Target, _errors));
        }

        public IComponentUpdater.IActionConnector IsBetween<TValue>(TValue minimum, TValue maximum, string message)
            where TValue : IComparable<TValue> {
            var validator = new Operations.Validations.IsBetween<TValue>(Attribute, minimum, maximum, message);
            return new ActionConnector(validator.Validate(Target, _errors));
        }

        public IComponentUpdater.IActionConnector IsGreaterThan<TValue>(TValue minimum, string message)
            where TValue : IComparable<TValue> {
            var validator = new Operations.Validations.IsGrater<TValue>(Attribute, minimum, message);
            return new ActionConnector(validator.Validate(Target, _errors));
        }

        public IComponentUpdater.IActionConnector IsGreaterOrEqualTo<TValue>(TValue minimum, string message)
            where TValue : IComparable<TValue> {
            var validator = new Operations.Validations.IsGraterOrEqual<TValue>(Attribute, minimum, message);
            return new ActionConnector(validator.Validate(Target, _errors));
        }

        public IComponentUpdater.IActionConnector IsLessThan<TValue>(TValue maximum, string message)
            where TValue : IComparable<TValue> {
            var validator = new Operations.Validations.IsLess<TValue>(Attribute, maximum, message);
            return new ActionConnector(validator.Validate(Target, _errors));
        }

        public IComponentUpdater.IActionConnector IsLessOrEqualTo<TValue>(TValue maximum, string message)
            where TValue : IComparable<TValue> {
            var validator = new Operations.Validations.IsLessOrEqual<TValue>(Attribute, maximum, message);
            return new ActionConnector(validator.Validate(Target, _errors));
        }
    }

    private class Conditional : ComponentUpdater, IComponentUpdater.IConditional {
        private readonly bool _previous;

        public Conditional(IComponent target, string attribute, bool previous = true)
            : base(target, attribute) {
            _previous = previous;
        }

        public IComponentUpdater.ILogicalConnector Contains<TValue>(TValue candidate) {
            var condition = new Operations.Assertions.Contains<TValue>(Attribute, candidate);
            return new LogicalConnector(Target, _previous && condition.IsTrueFor(Target));
        }

        public IComponentUpdater.ILogicalConnector ContainsKey<TKey, TValue>(TKey key)
            where TKey : notnull {
            var condition = new Operations.Assertions.ContainsKey<TKey, TValue>(Attribute, key);
            return new LogicalConnector(Target, _previous && condition.IsTrueFor(Target));
        }

        public IComponentUpdater.ILogicalConnector IsEqualTo<TValue>(TValue validValue)
            where TValue : IEquatable<TValue> {
            var condition = new Operations.Assertions.IsEqual<TValue>(Attribute, validValue);
            return new LogicalConnector(Target, _previous && condition.IsTrueFor(Target));
        }

        public IComponentUpdater.ILogicalConnector IsBetween<TValue>(TValue minimum, TValue maximum)
            where TValue : IComparable<TValue> {
            var condition = new Operations.Assertions.IsBetween<TValue>(Attribute, minimum, maximum);
            return new LogicalConnector(Target, _previous && condition.IsTrueFor(Target));
        }

        public IComponentUpdater.ILogicalConnector IsGreaterThan<TValue>(TValue minimum)
            where TValue : IComparable<TValue> {
            var condition = new Operations.Assertions.IsGrater<TValue>(Attribute, minimum);
            return new LogicalConnector(Target, _previous && condition.IsTrueFor(Target));
        }

        public IComponentUpdater.ILogicalConnector IsGreaterOrEqualTo<TValue>(TValue minimum)
            where TValue : IComparable<TValue> {
            var condition = new Operations.Assertions.IsGraterOrEqual<TValue>(Attribute, minimum);
            return new LogicalConnector(Target, _previous && condition.IsTrueFor(Target));
        }

        public IComponentUpdater.ILogicalConnector IsLessThan<TValue>(TValue maximum)
            where TValue : IComparable<TValue> {
            var condition = new Operations.Assertions.IsLess<TValue>(Attribute, maximum);
            return new LogicalConnector(Target, _previous && condition.IsTrueFor(Target));
        }

        public IComponentUpdater.ILogicalConnector IsLessOrEqualTo<TValue>(TValue maximum)
            where TValue : IComparable<TValue> {
            var condition = new Operations.Assertions.IsLessOrEqual<TValue>(Attribute, maximum);
            return new LogicalConnector(Target, _previous && condition.IsTrueFor(Target));
        }
    }

    private class LogicalConnector : ComponentUpdater, IComponentUpdater.ILogicalConnector {
        private readonly bool _result;

        public LogicalConnector(IComponent target, bool result)
            : base(target, null!) {
            _result = result;
        }

        public IComponentUpdater.IConditional And()
            => new Conditional(Target, Attribute, _result);

        public IComponentUpdater.IConditional And(string attribute)
            => new Conditional(Target, attribute, _result);

        public IComponentUpdater.IActionConnector Then(Action<IComponentUpdater.IMain> onTrue, Action<IComponentUpdater.IMain>? onFalse = null) {
            var newBranch = new Main(Target);
            if (_result)
                onTrue(newBranch);
            else
                onFalse?.Invoke(newBranch);
            return new ActionConnector(Target);
        }
    }
}
