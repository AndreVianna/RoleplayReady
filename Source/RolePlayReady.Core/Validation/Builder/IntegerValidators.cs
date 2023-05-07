namespace System.Validation.Builder;

public class IntegerValidators : Validators<int?>, IIntegerValidators {
    private readonly Connectors<int?, IntegerValidators> _connector;
    private readonly ValidationCommandFactory _commandFactory;

    public IntegerValidators(int? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        _connector = new Connectors<int?, IntegerValidators>(this);
        _commandFactory = ValidationCommandFactory.For(typeof(int?), Source, Result);
    }

    public IConnectors<int?, IntegerValidators> MinimumIs(int threshold) {
        _commandFactory.Create(nameof(IsLessThanCommand<int>), threshold).Negate(Subject);
        return _connector;
    }

    public IConnectors<int?, IntegerValidators> IsGreaterThan(int threshold) {
        _commandFactory.Create(nameof(IsGreaterThanCommand<int>), threshold).Validate(Subject);
        return _connector;
    }

    public IConnectors<int?, IntegerValidators> IsEqualTo(int threshold) {
        _commandFactory.Create(nameof(IsEqualToCommand<int>), threshold).Validate(Subject);
        return _connector;
    }

    public IConnectors<int?, IntegerValidators> IsLessThan(int threshold) {
        _commandFactory.Create(nameof(IsLessThanCommand<int>), threshold).Validate(Subject);
        return _connector;
    }

    public IConnectors<int?, IntegerValidators> MaximumIs(int threshold) {
        _commandFactory.Create(nameof(IsGreaterThanCommand<int>), threshold).Negate(Subject);
        return _connector;
    }
}