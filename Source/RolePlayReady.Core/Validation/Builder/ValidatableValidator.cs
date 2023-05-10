namespace System.Validation.Builder;
public class ValidatableValidator : Validator<IValidatable?>, IValidatableValidator {
    private readonly ValidationCommandFactory _commandFactory;

    public ValidatableValidator(IValidatable? subject, string source, ValidatorMode mode = ValidatorMode.And)
        : base(subject, source, mode) {
        Connector = new Connector<IValidatable?, ValidatableValidator>(this);
        _commandFactory = ValidationCommandFactory.For(typeof(IValidatable), Source);
    }

    public IConnector<ValidatableValidator> Connector { get; }

    public IConnector<ValidatableValidator> IsNull() {
        Result += _commandFactory.Create(nameof(IsNull)).Validate(Subject);
        return Connector;
    }

    public IConnector<ValidatableValidator> IsNotNull() {
        Result += _commandFactory.Create(nameof(IsNull)).Negate(Subject);
        return Connector;
    }

    public IConnector<ValidatableValidator> IsValid() {
        Result += _commandFactory.Create(nameof(IsValid)).Validate(Subject);
        return Connector;
    }
}