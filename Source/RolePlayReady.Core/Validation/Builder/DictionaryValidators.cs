namespace System.Validation.Builder;

public class DictionaryValidators<TKey, TValue>
    : Validators<IDictionary<TKey, TValue?>>
        , IDictionaryValidators<TKey, TValue>
    where TKey : notnull {
    private readonly Connectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> _connector;
    private readonly ValidationCommandFactory _commandFactory;

    public DictionaryValidators(IDictionary<TKey, TValue?>? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        _connector = new Connectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>>(this);
        _commandFactory = ValidationCommandFactory.For(typeof(IDictionary<TKey, TValue?>), Source, Result);
    }

    public IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> IsNotEmpty() {
        _commandFactory.Create(nameof(IsEmptyCommand)).Negate(Subject);
        return _connector;
    }

    public IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> MinimumCountIs(int size) {
        _commandFactory.Create(nameof(MinimumCountIsCommand<int>), size).Validate(Subject);
        return _connector;
    }

    public IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> CountIs(int size) {
        _commandFactory.Create(nameof(CountIsCommand<int>), size).Validate(Subject);
        return _connector;
    }

    public IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> ContainsKey(TKey key) {
        _commandFactory.Create(nameof(ContainsKeyCommand<int,int>), key).Validate(Subject);
        return _connector;
    }

    public IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> MaximumCountIs(int size) {
        _commandFactory.Create(nameof(MaximumCountIsCommand<int>), size).Validate(Subject);
        return _connector;
    }

    public IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> ForEach(Func<TValue?, ITerminator> validateUsing) {
        if (Subject is null)
            return _connector;
        foreach (var key in Subject.Keys)
            AddItemErrors(validateUsing(Subject[key]).Result.Errors, $"{Source}[{key}]");
        return _connector;
    }

    private void AddItemErrors(IEnumerable<ValidationError> errors, string source) {
        foreach (var error in errors) {
            var path = ((string)error.Arguments[0]!).Split('.');
            error.Arguments[0] = $"{source}.{string.Join('.', path[1..])}";
            Result.Errors.Add(error);
        }
    }
}