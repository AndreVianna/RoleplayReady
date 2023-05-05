using System.Validation.Abstractions;

namespace System.Validation;

public sealed class CollectionValidators<TItem>
    : Validators<ICollection<TItem?>, CollectionValidators<TItem>>
    , ICollectionValidators<TItem> {

    public static CollectionValidators<TSubject> CreateAsRequired<TSubject>(ICollection<TSubject?>? subject, string source)
        => new(subject, source, EnsureNotNull(subject, source));

    private CollectionValidators(ICollection<TItem?>? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        Connector = new Connectors<ICollection<TItem?>, CollectionValidators<TItem>>(Subject, this);
    }

    public IValidatorsConnector<ICollection<TItem?>, CollectionValidators<TItem>> IsNotEmpty() {
        CommandFactory.Create(nameof(IsNotEmpty)).Validate();
        return Connector;
    }

    public IValidatorsConnector<ICollection<TItem?>, CollectionValidators<TItem>> MinimumCountIs(int size) {
        CommandFactory.Create(nameof(MinimumCountIs), size).Validate();
        return Connector;
    }

    public IValidatorsConnector<ICollection<TItem?>, CollectionValidators<TItem>> CountIs(int size) {
        CommandFactory.Create(nameof(CountIs), size).Validate();
        return Connector;
    }

    public IValidatorsConnector<ICollection<TItem?>, CollectionValidators<TItem>> Contains(TItem? item) {
        CommandFactory.Create(nameof(Contains), item).Validate();
        return Connector;
    }

    public IValidatorsConnector<ICollection<TItem?>, CollectionValidators<TItem>> MaximumCountIs(int size) {
        CommandFactory.Create(nameof(MaximumCountIs), size).Validate();
        return Connector;
    }

    public IValidatorsConnector<ICollection<TItem?>, CollectionValidators<TItem>> ForEach(Func<TItem?, IValidatorsConnector<TItem?, IValidators>> validate) {
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
            Result.Errors.Add(error);
        }
    }
}