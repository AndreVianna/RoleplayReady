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
        Result += Result += _commandFactory.Create(nameof(IsNull)).Validate(Subject);
        return Connector;
    }

    public IConnector<IntegerValidator> IsNotNull() {
        Result += _commandFactory.Create(nameof(IsNull)).Negate(Subject);
        return Connector;
    }

    public IConnector<IntegerValidator> MinimumIs(int threshold) {
        Result += _commandFactory.Create(nameof(IsLessThan), threshold).Negate(Subject);
        return Connector;
    }

    public IConnector<IntegerValidator> IsGreaterThan(int threshold) {
        Result += _commandFactory.Create(nameof(IsGreaterThan), threshold).Validate(Subject);
        return Connector;
    }

    public IConnector<IntegerValidator> IsEqualTo(int threshold) {
        Result += _commandFactory.Create(nameof(IsEqualTo), threshold).Validate(Subject);
        return Connector;
    }

    public IConnector<IntegerValidator> IsLessThan(int threshold) {
        Result += _commandFactory.Create(nameof(IsLessThan), threshold).Validate(Subject);
        return Connector;
    }

    public IConnector<IntegerValidator> MaximumIs(int threshold) {
        Result += _commandFactory.Create(nameof(IsGreaterThan), threshold).Negate(Subject);
        return Connector;
    }
}