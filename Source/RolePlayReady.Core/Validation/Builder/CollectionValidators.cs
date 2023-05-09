namespace System.Validation.Builder;

public sealed class CollectionValidators<TItem> : Validators<ICollection<TItem?>>, ICollectionValidators<TItem> {
    private readonly Connectors<ICollection<TItem?>, CollectionValidators<TItem>> _connector;
    private readonly ValidationCommandFactory _commandFactory;

    public CollectionValidators(ICollection<TItem?>? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        _connector = new Connectors<ICollection<TItem?>, CollectionValidators<TItem>>(this);
        _commandFactory = ValidationCommandFactory.For(typeof(ICollection<TItem?>), Source, Result);
    }

    public IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> IsNotEmpty() {
        _commandFactory.Create(nameof(IsEmptyCommand)).Negate(Subject);
        return _connector;
    }

    public IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> HasAtLeast(int size) {
        _commandFactory.Create(nameof(HasAtLeastCommand<int>), size).Validate(Subject);
        return _connector;
    }

    public IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> Has(int size) {
        _commandFactory.Create(nameof(HasCommand<int>), size).Validate(Subject);
        return _connector;
    }

    public IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> Contains(TItem item) {
        _commandFactory.Create(nameof(ContainsCommand), item).Validate(Subject);
        return _connector;
    }

    public IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> AtMostHas(int size) {
        _commandFactory.Create(nameof(HasAtMostCommand<int>), size).Validate(Subject);
        return _connector;
    }

    public IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> Each(Func<TItem?, ITerminator> validate) {
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