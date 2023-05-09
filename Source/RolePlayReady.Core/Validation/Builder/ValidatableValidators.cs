namespace System.Validation.Builder;
public class ValidatableValidators : Validators<IValidatable?>, IValidatableValidators {
    private readonly Connectors<IValidatable?, ValidatableValidators> _connector;
    private readonly ValidationCommandFactory _commandFactory;

    public ValidatableValidators(IValidatable? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        _connector = new Connectors<IValidatable?, ValidatableValidators>(this);
        _commandFactory = ValidationCommandFactory.For(typeof(IValidatable), Source, Result);
    }

    public IConnectors<IValidatable?, IValidators> IsValid() {
        _commandFactory.Create(nameof(IsValid)).Validate(Subject);
        return _connector;
    }
}