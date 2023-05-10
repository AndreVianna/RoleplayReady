namespace System.Validation.Builder;

public class DecimalValidator : Validator<decimal?>, IDecimalValidator {
    private readonly ValidationCommandFactory _commandFactory;

    public DecimalValidator(decimal? subject, string source, ValidatorMode mode = ValidatorMode.And)
        : base(subject, source, mode) {
        Connector = new Connector<decimal?, DecimalValidator>(this);
        _commandFactory = ValidationCommandFactory.For(typeof(decimal?), Source);
    }

    public IConnector<DecimalValidator> Connector { get; }

    public IConnector<DecimalValidator> IsNull() {
        var validator = _commandFactory.Create(nameof(IsNull));
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<DecimalValidator> IsNotNull() {
        var validator = _commandFactory.Create(nameof(IsNull));
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<DecimalValidator> MinimumIs(decimal threshold) {
        var validator = _commandFactory.Create(nameof(IsLessThan), threshold);
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<DecimalValidator> IsGreaterThan(decimal threshold) {
        var validator = _commandFactory.Create(nameof(IsGreaterThan), threshold);
        ValidateWith(validator);
        Result += _commandFactory.Create(nameof(IsGreaterThan), threshold).Validate(Subject);
        return Connector;
    }

    public IConnector<DecimalValidator> IsEqualTo(decimal threshold) {
        var validator = _commandFactory.Create(nameof(IsEqualTo), threshold);
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<DecimalValidator> IsLessThan(decimal threshold) {
        var validator = _commandFactory.Create(nameof(IsLessThan), threshold);
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<DecimalValidator> MaximumIs(decimal threshold) {
        var validator = _commandFactory.Create(nameof(IsGreaterThan), threshold);
        ValidateWith(validator);
        return Connector;
    }
}