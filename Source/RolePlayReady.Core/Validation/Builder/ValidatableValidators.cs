namespace System.Validation.Builder;
public class ValidatableValidators : Validators<IValidatable?>, IValidatableValidators {
    private readonly Connectors<IValidatable?, ValidatableValidators> _connector;
    private readonly ValidationCommandFactory<IValidatable> _commandFactory;

    public static ValidatableValidators CreateAsOptional(IValidatable? subject, string source)
        => new(subject, source);
    public static ValidatableValidators CreateAsRequired(IValidatable? subject, string source)
        => new(subject, source, EnsureNotNull(subject, source));

    private ValidatableValidators(IValidatable? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        _connector = new Connectors<IValidatable?, ValidatableValidators>(this);
        _commandFactory = ValidationCommandFactory.For(Subject, Source, Result);
    }

    public IConnectors<IValidatable?, IValidators> IsValid() {
        _commandFactory.Create(nameof(IsValidCommand)).Validate();
        return _connector;
    }
}