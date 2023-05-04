namespace System.Validations;

public class DictionaryValidations<TKey, TValue>
    : Validations<IDictionary<TKey, TValue?>, DictionaryValidations<TKey, TValue>>
        , IDictionaryValidations<TKey, TValue>
    where TKey : notnull {

    public static DictionaryValidations<TSubjectKey, TSubjectValue> CreateAsRequired<TSubjectKey, TSubjectValue>(IDictionary<TSubjectKey, TSubjectValue?>? subject, string source)
        where TSubjectKey : notnull
        => new(subject, source, EnsureNotNull(subject, source));

    private DictionaryValidations(IDictionary<TKey, TValue?>? subject, string source, IEnumerable<ValidationError>? previousErrors = null)
        : base(ValidationMode.None, subject, source, previousErrors) {
        Connector = new ValidationsConnector<IDictionary<TKey, TValue?>, DictionaryValidations<TKey, TValue>>(Subject, this);
    }

    public IValidationsConnector<IDictionary<TKey, TValue?>, DictionaryValidations<TKey, TValue>> IsNotEmpty() {
        if (Subject is not null && !Subject.Any())
            Errors.Add(new(CannotBeEmpty, Source));

        return Connector;
    }

    public IValidationsConnector<IDictionary<TKey, TValue?>, DictionaryValidations<TKey, TValue>> MinimumCountIs(int size) {
        if (Subject is not null && Subject.Count < size)
            Errors.Add(new(CannotHaveLessThan, Source, size, Subject.Count));

        return Connector;
    }

    public IValidationsConnector<IDictionary<TKey, TValue?>, DictionaryValidations<TKey, TValue>> CountIs(int size) {
        if (Subject is not null && Subject.Count != size)
            Errors.Add(new(MustHave, Source, size, Subject.Count));

        return Connector;
    }

    public IValidationsConnector<IDictionary<TKey, TValue?>, DictionaryValidations<TKey, TValue>> ContainsKey(TKey key) {
        if (Subject is not null && !Subject.ContainsKey(key))
            Errors.Add(new(MustContainKey, Source, key));

        return Connector;
    }

    public IValidationsConnector<IDictionary<TKey, TValue?>, DictionaryValidations<TKey, TValue>> NotContainsKey(TKey key) {
        if (Subject is not null && Subject.ContainsKey(key))
            Errors.Add(new(MustNotContainKey, Source, key));

        return Connector;
    }

    public IValidationsConnector<IDictionary<TKey, TValue?>, DictionaryValidations<TKey, TValue>> MaximumCountIs(int size) {
        if (Subject is not null && Subject.Count > size)
            Errors.Add(new(CannotHaveMoreThan, Source, size, Subject.Count));

        return Connector;
    }

    public IValidationsConnector<IDictionary<TKey, TValue?>, DictionaryValidations<TKey, TValue>> ForEach(Func<TValue?, IValidationsConnector<TValue?, IValidations>> validateUsing) {
        if (Subject is null) return Connector;
        foreach (var key in Subject.Keys)
            AddItemErrors(validateUsing(Subject[key]).Result.Errors, $"{Source}[{key}]");
        return Connector;
    }

    private void AddItemErrors(IEnumerable<ValidationError> errors, string source) {
        foreach (var error in errors) {
            var path = ((string)error.Arguments[0]!).Split('.');
            error.Arguments[0] = $"{source}.{string.Join('.', path[1..])}";
            Errors.Add(error);
        }
    }
}