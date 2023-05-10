namespace System.Validation.Builder;

public class IntegerValidator : Validator<int?>, IIntegerValidator {
    private readonly ValidationCommandFactory _commandFactory;

    public IntegerValidator(int? subject, string source, ValidatorMode mode = ValidatorMode.And)
        : base(subject, source, mode) {
        Connector = new Connector<int?, IntegerValidator>(this);
        _commandFactory = ValidationCommandFactory.For(typeof(int?), Source);
    }

    public IConnector<IntegerValidator> Connector { get; }

    public IConnector<IntegerValidator> IsNull() {
        var validator = _commandFactory.Create(nameof(IsNull));
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<IntegerValidator> IsNotNull() {
        var validator = _commandFactory.Create(nameof(IsNull));
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<IntegerValidator> MinimumIs(int threshold) {
        var validator = _commandFactory.Create(nameof(IsLessThan), threshold);
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<IntegerValidator> IsGreaterThan(int threshold) {
        var validator = _commandFactory.Create(nameof(IsGreaterThan), threshold);
        ValidateWith(validator);
        Result += _commandFactory.Create(nameof(IsGreaterThan), threshold).Validate(Subject);
        return Connector;
    }

    public IConnector<IntegerValidator> IsEqualTo(int threshold) {
        var validator = _commandFactory.Create(nameof(IsEqualTo), threshold);
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<IntegerValidator> IsLessThan(int threshold) {
        var validator = _commandFactory.Create(nameof(IsLessThan), threshold);
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<IntegerValidator> MaximumIs(int threshold) {
        var validator = _commandFactory.Create(nameof(IsGreaterThan), threshold);
        ValidateWith(validator);
        return Connector;
    }
}