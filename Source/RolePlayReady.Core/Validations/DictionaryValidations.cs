namespace System.Validations;

public class DictionaryValidations<TKey, TValue> : IDictionaryValidations<TKey, TValue?>
    where TKey : notnull {
    private readonly Connects<DictionaryValidations<TKey, TValue>> _connector;

    public DictionaryValidations(IDictionary<TKey, TValue?>? subject, string source, IEnumerable<ValidationError>? previousErrors = null) {
        Subject = subject;
        Source = source;
        Errors = previousErrors?.ToList() ?? new List<ValidationError>();
        _connector = new Connects<DictionaryValidations<TKey, TValue>>(this);
    }

    public IDictionary<TKey, TValue?>? Subject { get; }
    public string Source { get; }
    public List<ValidationError> Errors { get; }

    public IConnects<IDictionaryValidations<TKey, TValue?>> IsNotEmpty() {
        if (Subject is not null && !Subject.Any())
            Errors.Add(new(CannotBeEmpty, Source));

        return _connector;
    }

    public IConnects<IDictionaryValidations<TKey, TValue?>> MinimumCountIs(int size) {
        if (Subject is not null && Subject.Count < size)
            Errors.Add(new(CannotHaveLessThan, Source, size, Subject.Count));

        return _connector;
    }

    public IConnects<IDictionaryValidations<TKey, TValue?>> CountIs(int size) {
        if (Subject is not null && Subject.Count != size)
            Errors.Add(new(MustHave, Source, size, Subject.Count));

        return _connector;
    }

    public IConnects<IDictionaryValidations<TKey, TValue?>> ContainsKey(TKey key) {
        if (Subject is not null && !Subject.ContainsKey(key))
            Errors.Add(new(MustContainKey, Source, key));

        return _connector;
    }

    public IConnects<IDictionaryValidations<TKey, TValue?>> NotContainsKey(TKey key) {
        if (Subject is not null && Subject.ContainsKey(key))
            Errors.Add(new(MustNotContainKey, Source, key));

        return _connector;
    }

    public IConnects<IDictionaryValidations<TKey, TValue?>> MaximumCountIs(int size) {
        if (Subject is not null && Subject.Count > size)
            Errors.Add(new(CannotHaveMoreThan, Source, size, Subject.Count));

        return _connector;
    }

    public ICollection<ValidationError> ForEach(Func<TValue?, ICollection<ValidationError>> validateUsing) {
        if (Subject is null) return Errors;
        foreach (var key in Subject.Keys)
            AddItemErrors(validateUsing(Subject[key]), $"{Source}[{key}]");
        return Errors;
    }

    private void AddItemErrors(IEnumerable<ValidationError> errors, string source) {
        foreach (var error in errors) {
            var path = ((string)error.Arguments[0]!).Split('.');
            error.Arguments[0] = $"{source}.{string.Join('.', path[1..])}";
            Errors.Add(error);
        }
    }
}