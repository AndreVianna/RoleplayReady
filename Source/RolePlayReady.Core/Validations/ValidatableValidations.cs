namespace System.Validations;
public class ValidatableValidations
    : Validations<IValidatable?, ValidatableValidations>
        , IValidatableValidations {

    public static ValidatableValidations CreateAsOptional(IValidatable? subject, string source)
        => new(subject, source);
    public static ValidatableValidations CreateAsRequired(IValidatable? subject, string source)
        => new(subject, source, EnsureNotNull(subject, source));

    private ValidatableValidations(IValidatable? subject, string source, IEnumerable<ValidationError>? previousErrors = null)
        : base(ValidationMode.None, subject, source, previousErrors) {
        Connector = new ValidationsConnector<IValidatable?, ValidatableValidations>(Subject, this);
    }


    public IValidationsConnector<IValidatable?, IValidations> IsValid() {
        if (Subject is null) return Connector;
        var validation = Subject.ValidateSelf();
        foreach (var error in validation.Errors) {
            error.Arguments[0] = $"{Source}.{error.Arguments[0]}";
            Errors.Add(error);
        }

        return Connector;
    }
}