namespace System.Validation.Builder;

public sealed class CollectionValidators<TItem> : Validators<ICollection<TItem?>>, ICollectionValidators<TItem> {
    private readonly Connectors<ICollection<TItem?>, CollectionValidators<TItem>> _connector;
    private readonly ValidationCommandFactory<ICollection<TItem?>> _commandFactory;

    public static CollectionValidators<TSubject> Create<TSubject>(ICollection<TSubject?>? subject, string source)
        => new(subject, source, EnsureNotNull(subject, source));

    public static Connectors<ICollection<TSubject?>, CollectionValidators<TSubject>> CreatedAndConnect<TSubject>(ICollection<TSubject?>? subject, string source)
        => new (Create(subject, source));

    private CollectionValidators(ICollection<TItem?>? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        _connector = new Connectors<ICollection<TItem?>, CollectionValidators<TItem>>(this);
        _commandFactory = ValidationCommandFactory.For(Subject, Source, Result);
    }

    public IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> IsNotEmpty() {
        _commandFactory.Create(nameof(IsEmptyCommand)).Negate();
        return _connector;
    }

    public IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> MinimumCountIs(int size) {
        _commandFactory.Create(nameof(MinimumCountIsCommand<int>), size).Validate();
        return _connector;
    }

    public IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> CountIs(int size) {
        _commandFactory.Create(nameof(CountIsCommand<int>), size).Validate();
        return _connector;
    }

    public IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> Contains(TItem item) {
        _commandFactory.Create(nameof(ContainsCommand), item).Validate();
        return _connector;
    }

    public IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> MaximumCountIs(int size) {
        _commandFactory.Create(nameof(MaximumCountIsCommand<int>), size).Validate();
        return _connector;
    }

    public IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> ForEach(Func<TItem?, ITerminator> validate) {
        if (Subject is null) return _connector;
        var index = 0;
        foreach (var item in Subject)
            AddItemErrors(validate(item).Result.Errors, $"{Source}[{index++}]");
        return _connector;
    }

    private void AddItemErrors(IEnumerable<ValidationError> errors, string source) {
        foreach (var error in errors) {
            var path = ((string)error.Arguments[0]!).Split('.');
            error.Arguments[0] = path.Length > 1 ? $"{source}.{string.Join('.', path[1..])}" : source;
            Result.Errors.Add(error);
        }
    }
}