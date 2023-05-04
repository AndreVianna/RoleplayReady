namespace System.Validations;

public class CollectionValidations<TItem> : ICollectionValidations<TItem?> {
    private readonly Connects<CollectionValidations<TItem>> _connector;

    public CollectionValidations(ICollection<TItem?>? subject, string source, IEnumerable<ValidationError>? previousErrors = null) {
        Subject = subject;
        Source = source;
        Errors = previousErrors?.ToList() ?? new List<ValidationError>();
        _connector = new Connects<CollectionValidations<TItem>>(this);
    }

    public ICollection<TItem?>? Subject { get; }
    public string Source { get; }
    public List<ValidationError> Errors { get; }

    public IConnects<ICollectionValidations<TItem?>> IsNotEmpty() {
        if (Subject is not null && !Subject.Any())
            Errors.Add(new(CannotBeEmpty, Source));

        return _connector;
    }

    public IConnects<ICollectionValidations<TItem?>> MinimumCountIs(int size) {
        if (Subject is not null && Subject.Count < size)
            Errors.Add(new(CannotHaveLessThan, Source, size, Subject.Count));

        return _connector;
    }

    public IConnects<ICollectionValidations<TItem?>> CountIs(int size) {
        if (Subject is not null && Subject.Count != size)
            Errors.Add(new(MustHave, Source, size, Subject.Count));

        return _connector;
    }

    public IConnects<ICollectionValidations<TItem?>> Contains(TItem? item) {
        if (Subject is not null && !Subject.Contains(item))
            Errors.Add(new(MustContain, Source, item));

        return _connector;
    }

    public IConnects<ICollectionValidations<TItem?>> NotContains(TItem? item) {
        if (Subject is not null && Subject.Contains(item))
            Errors.Add(new(MustNotContain, Source, item));

        return _connector;
    }

    public IConnects<ICollectionValidations<TItem?>> MaximumCountIs(int size) {
        if (Subject is not null && Subject.Count > size)
            Errors.Add(new(CannotHaveMoreThan, Source, size, Subject.Count));

        return _connector;
    }

    public ICollection<ValidationError> ForEach(Func<TItem?, ICollection<ValidationError>> validateUsing) {
        if (Subject is null) return Errors;
        var index = 0;
        foreach (var item in Subject)
            AddItemErrors(validateUsing(item), $"{Source}[{index++}]");
        return Errors;
    }

    private void AddItemErrors(IEnumerable<ValidationError> errors, string source) {
        foreach (var error in errors) {
            var path = ((string)error.Arguments[0]!).Split('.');
            error.Arguments[0] = path.Length > 1 ? $"{source}.{string.Join('.', path[1..])}" : source;
            Errors.Add(error);
        }
    }
}