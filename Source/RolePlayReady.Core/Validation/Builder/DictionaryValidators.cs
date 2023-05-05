namespace System.Validation.Builder;

public class DictionaryValidators<TKey, TValue>
    : Validators<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>>
        , IDictionaryValidators<TKey, TValue>
    where TKey : notnull {

    public static DictionaryValidators<TSubjectKey, TSubjectValue> Create<TSubjectKey, TSubjectValue>(IDictionary<TSubjectKey, TSubjectValue?>? subject, string source)
        where TSubjectKey : notnull
        => new(subject, source, EnsureNotNull(subject, source));

    public static Connectors<IDictionary<TSubjectKey, TSubjectValue?>, DictionaryValidators<TSubjectKey, TSubjectValue>> CreatedAndConnect<TSubjectKey, TSubjectValue>(IDictionary<TSubjectKey, TSubjectValue?>? subject, string source)
        where TSubjectKey : notnull {
        var validation = new DictionaryValidators<TSubjectKey, TSubjectValue>(subject, source, EnsureNotNull(subject, source));
        return new Connectors<IDictionary<TSubjectKey, TSubjectValue?>, DictionaryValidators<TSubjectKey, TSubjectValue>>(validation.Subject, validation);
    }

    private DictionaryValidators(IDictionary<TKey, TValue?>? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        Connector = new Connectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>>(Subject, this);
    }

    public IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> IsNotEmpty() {
        CommandFactory.Create(nameof(IsNotEmpty)).Validate();
        return Connector;
    }

    public IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> MinimumCountIs(int size) {
        CommandFactory.Create(nameof(MinimumCountIs), size).Validate();
        return Connector;
    }

    public IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> CountIs(int size) {
        CommandFactory.Create(nameof(CountIs), size).Validate();
        return Connector;
    }

    public IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> ContainsKey(TKey key) {
        CommandFactory.Create(nameof(ContainsKey), key).Validate();
        return Connector;
    }

    public IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> MaximumCountIs(int size) {
        CommandFactory.Create(nameof(MaximumCountIs), size).Validate();
        return Connector;
    }

    public IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> ForEach(Func<TValue?, IConnectors<TValue?, IValidators>> validateUsing) {
        if (Subject is null)
            return Connector;
        foreach (var key in Subject.Keys)
            AddItemErrors(validateUsing(Subject[key]).Result.Errors, $"{Source}[{key}]");
        return Connector;
    }

    private void AddItemErrors(IEnumerable<ValidationError> errors, string source) {
        foreach (var error in errors) {
            var path = ((string)error.Arguments[0]!).Split('.');
            error.Arguments[0] = $"{source}.{string.Join('.', path[1..])}";
            Result.Errors.Add(error);
        }
    }
}