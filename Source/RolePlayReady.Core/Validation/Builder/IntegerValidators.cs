namespace System.Validation.Builder;

public class IntegerValidators : Validators<int?>, IIntegerValidators {
    private readonly Connectors<int?, IntegerValidators> _connector;
    private readonly ValidationCommandFactory<int?> _commandFactory;

    public static IntegerValidators CreateAsOptional(int? subject, string source)
        => new(subject, source);
    public static IntegerValidators CreateAsRequired(int? subject, string source)
        => new(subject, source, EnsureNotNull(subject, source));

    private IntegerValidators(int? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        _connector = new Connectors<int?, IntegerValidators>(this);
        _commandFactory = ValidationCommandFactory.For(Subject, Source, Result);
    }

    public IConnectors<int?, IntegerValidators> MinimumIs(int threshold) {
        _commandFactory.Create(nameof(IsLessThanCommand<int>), threshold).Negate();
        return _connector;
    }

    public IConnectors<int?, IntegerValidators> IsGreaterThan(int threshold) {
        _commandFactory.Create(nameof(IsGreaterThanCommand<int>), threshold).Validate();
        return _connector;
    }

    public IConnectors<int?, IntegerValidators> IsEqualTo(int threshold) {
        _commandFactory.Create(nameof(IsEqualToCommand<int>), threshold).Validate();
        return _connector;
    }

    public IConnectors<int?, IntegerValidators> IsLessThan(int threshold) {
        _commandFactory.Create(nameof(IsLessThanCommand<int>), threshold).Validate();
        return _connector;
    }

    public IConnectors<int?, IntegerValidators> MaximumIs(int threshold) {
        _commandFactory.Create(nameof(IsGreaterThanCommand<int>), threshold).Negate();
        return _connector;
    }
}