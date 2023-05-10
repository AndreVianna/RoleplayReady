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
        Result += _commandFactory.Create(nameof(IsNull)).Validate(Subject);
        return Connector;
    }

    public IConnector<DecimalValidator> IsNotNull() {
        Result += _commandFactory.Create(nameof(IsNull)).Negate(Subject);
        return Connector;
    }

    public IConnector<DecimalValidator> MinimumIs(decimal threshold) {
        Result += _commandFactory.Create(nameof(IsLessThan), threshold).Negate(Subject);
        return Connector;
    }

    public IConnector<DecimalValidator> IsGreaterThan(decimal threshold) {
        Result += _commandFactory.Create(nameof(IsGreaterThan), threshold).Validate(Subject);
        return Connector;
    }

    public IConnector<DecimalValidator> IsEqualTo(decimal threshold) {
        Result += _commandFactory.Create(nameof(IsEqualTo), threshold).Validate(Subject);
        return Connector;
    }

    public IConnector<DecimalValidator> IsLessThan(decimal threshold) {
        Result += _commandFactory.Create(nameof(IsLessThan), threshold).Validate(Subject);
        return Connector;
    }

    public IConnector<DecimalValidator> MaximumIs(decimal threshold) {
        Result += _commandFactory.Create(nameof(IsGreaterThan), threshold).Negate(Subject);
        return Connector;
    }
}