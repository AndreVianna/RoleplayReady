﻿namespace System.Validation.Builder;

public sealed class CollectionValidator<TItem>
    : Validator<ICollection<TItem?>>, ICollectionValidator<TItem> {
    private readonly ValidationCommandFactory _commandFactory;

    public CollectionValidator(ICollection<TItem?>? subject, string source, ValidatorMode mode = ValidatorMode.And)
        : base(subject, source, mode) {
        Connector = new Connector<ICollection<TItem?>, CollectionValidator<TItem>>(this);
        _commandFactory = ValidationCommandFactory.For(typeof(ICollection<TItem?>), Source);
    }

    public IConnector<CollectionValidator<TItem>> Connector { get; }

    public IConnector<CollectionValidator<TItem>> IsNull() {
        var validator = _commandFactory.Create(nameof(IsNull));
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<CollectionValidator<TItem>> IsNotNull() {
        Negate();
        return IsNull();
    }

    public IConnector<CollectionValidator<TItem>> IsEmpty() {
        var validator = _commandFactory.Create(nameof(IsEmpty));
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<CollectionValidator<TItem>> IsNotEmpty() {
        Negate();
        return IsEmpty();
    }

    public IConnector<CollectionValidator<TItem>> HasAtLeast(int size) {
        var validator = _commandFactory.Create(nameof(HasAtLeast), size);
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<CollectionValidator<TItem>> Has(int size) {
        var validator = _commandFactory.Create(nameof(Has), size);
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<CollectionValidator<TItem>> Contains(TItem item) {
        var validator = _commandFactory.Create(nameof(Contains), item);
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<CollectionValidator<TItem>> HasAtMost(int size) {
        var validator = _commandFactory.Create(nameof(HasAtMost), size);
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<CollectionValidator<TItem>> Each(Func<TItem?, ITerminator> validate) {
        if (Subject is null)
            return Connector;
        var index = 0;
        foreach (var item in Subject)
            AddItemErrors(validate(item).Result.Errors, $"{Source}[{index++}]");
        return Connector;
    }

    private void AddItemErrors(IEnumerable<ValidationError> errors, string source) {
        foreach (var error in errors) {
            var path = ((string)error.Arguments[0]!).Split('.');
            error.Arguments[0] = path.Length > 1 ? $"{source}.{string.Join('.', path[1..])}" : source;
            AddError(error);
        }
    }
}