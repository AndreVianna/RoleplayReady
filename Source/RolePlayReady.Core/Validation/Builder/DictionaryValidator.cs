namespace System.Validation.Builder;

public class DictionaryValidator<TKey, TValue>
    : Validator<IDictionary<TKey, TValue?>>
        , IDictionaryValidator<TKey, TValue>
    where TKey : notnull {
    private readonly ValidationCommandFactory _commandFactory;

    public DictionaryValidator(IDictionary<TKey, TValue?>? subject, string source, ValidatorMode mode = ValidatorMode.And)
        : base(subject, source, mode) {
        Connector = new Connector<IDictionary<TKey, TValue?>, DictionaryValidator<TKey, TValue>>(this);
        _commandFactory = ValidationCommandFactory.For(typeof(IDictionary<TKey, TValue?>), Source);
    }

    public IConnector<DictionaryValidator<TKey, TValue>> Connector { get; }

    public IConnector<DictionaryValidator<TKey, TValue>> IsNull() {
        Result += _commandFactory.Create(nameof(IsNull)).Validate(Subject);
        return Connector;
    }

    public IConnector<DictionaryValidator<TKey, TValue>> IsNotNull() {
        Result += _commandFactory.Create(nameof(IsNull)).Negate(Subject);
        return Connector;
    }

    public IConnector<DictionaryValidator<TKey, TValue>> IsEmpty() {
        Result += _commandFactory.Create(nameof(IsEmpty)).Validate(Subject);
        return Connector;
    }

    public IConnector<DictionaryValidator<TKey, TValue>> IsNotEmpty() {
        Result += _commandFactory.Create(nameof(IsEmpty)).Negate(Subject);
        return Connector;
    }

    public IConnector<DictionaryValidator<TKey, TValue>> HasAtLeast(int size) {
        Result += _commandFactory.Create(nameof(HasAtLeast), size).Validate(Subject);
        return Connector;
    }

    public IConnector<DictionaryValidator<TKey, TValue>> Has(int size) {
        Result += _commandFactory.Create(nameof(Has), size).Validate(Subject);
        return Connector;
    }

    public IConnector<DictionaryValidator<TKey, TValue>> ContainsKey(TKey key) {
        Result += _commandFactory.Create(nameof(ContainsKey), key).Validate(Subject);
        return Connector;
    }

    public IConnector<DictionaryValidator<TKey, TValue>> HasAtMost(int size) {
        Result += _commandFactory.Create(nameof(HasAtMost), size).Validate(Subject);
        return Connector;
    }

    public IConnector<DictionaryValidator<TKey, TValue>> Each(Func<TValue?, ITerminator> validateUsing) {
        if (Subject is null) return Connector;
        foreach (var key in Subject.Keys)
            AddItemErrors(validateUsing(Subject[key]).Result.Errors, $"{Source}[{key}]");
        return Connector;
    }

    private void AddItemErrors(IEnumerable<ValidationError> errors, string source) {
        foreach (var error in errors) {
            var path = ((string)error.Arguments[0]!).Split('.');
            error.Arguments[0] = $"{source}.{string.Join('.', path[1..])}";
            Result += error;
        }
    }
}