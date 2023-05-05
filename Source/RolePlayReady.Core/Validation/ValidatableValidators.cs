using System.Validation.Abstractions;

namespace System.Validation;
public class ValidatableValidators
    : Validators<IValidatable?, ValidatableValidators>
        , IValidatableValidators {

    public static ValidatableValidators CreateAsOptional(IValidatable? subject, string source)
        => new(subject, source);
    public static ValidatableValidators CreateAsRequired(IValidatable? subject, string source)
        => new(subject, source, EnsureNotNull(subject, source));

    private ValidatableValidators(IValidatable? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        Connector = new Connectors<IValidatable?, ValidatableValidators>(Subject, this);
    }

    public IValidatorsConnector<IValidatable?, IValidators> IsValid() {
        CommandFactory.Create(nameof(IsValid)).Validate();
        return Connector;
    }
}