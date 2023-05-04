namespace System.Validations;

public sealed class CollectionValidations<TItem> 
    : Validations<ICollection<TItem?>, CollectionValidations<TItem>>
        , ICollectionValidations<TItem> {
    public static CollectionValidations<TSubject> CreateAsRequired<TSubject>(ICollection<TSubject?>? subject, string source)
        => new(subject, source, EnsureNotNull(subject, source));

    private CollectionValidations(ICollection<TItem?>? subject, string source, IEnumerable<ValidationError>? previousErrors = null)
        : base(ValidationMode.None, subject, source, previousErrors) {
        Connector = new ValidationsConnector<ICollection<TItem?>, CollectionValidations<TItem>>(Subject, this);
    }

    public IValidationsConnector<ICollection<TItem?>, CollectionValidations<TItem>> IsNotEmpty() {
        if (Subject is not null && !Subject.Any())
            Errors.Add(new(CannotBeEmpty, Source));

        return Connector;
    }

    public IValidationsConnector<ICollection<TItem?>, CollectionValidations<TItem>> MinimumCountIs(int size) {
        if (Subject is not null && Subject.Count < size)
            Errors.Add(new(CannotHaveLessThan, Source, size, Subject.Count));

        return Connector;
    }

    public IValidationsConnector<ICollection<TItem?>, CollectionValidations<TItem>> CountIs(int size) {
        if (Subject is not null && Subject.Count != size)
            Errors.Add(new(MustHave, Source, size, Subject.Count));

        return Connector;
    }

    public IValidationsConnector<ICollection<TItem?>, CollectionValidations<TItem>> Contains(TItem? item) {
        if (Subject is not null && !Subject.Contains(item))
            Errors.Add(new(MustContain, Source, item));

        return Connector;
    }

    public IValidationsConnector<ICollection<TItem?>, CollectionValidations<TItem>> NotContains(TItem? item) {
        if (Subject is not null && Subject.Contains(item))
            Errors.Add(new(MustNotContain, Source, item));

        return Connector;
    }

    public IValidationsConnector<ICollection<TItem?>, CollectionValidations<TItem>> MaximumCountIs(int size) {
        if (Subject is not null && Subject.Count > size)
            Errors.Add(new(CannotHaveMoreThan, Source, size, Subject.Count));

        return Connector;
    }

    public IValidationsConnector<ICollection<TItem?>, CollectionValidations<TItem>> ForEach(Func<TItem?, IValidationsConnector<TItem?, IValidations>> validate) {
        if (Subject is null) return Connector;
        var index = 0;
        foreach (var item in Subject)
            AddItemErrors(validate(item).Result.Errors, $"{Source}[{index++}]");
        return Connector;
    }

    private void AddItemErrors(IEnumerable<ValidationError> errors, string source) {
        foreach (var error in errors) {
            var path = ((string)error.Arguments[0]!).Split('.');
            error.Arguments[0] = path.Length > 1 ? $"{source}.{string.Join('.', path[1..])}" : source;
            Errors.Add(error);
        }
    }
}