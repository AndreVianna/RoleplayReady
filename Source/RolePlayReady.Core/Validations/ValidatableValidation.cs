namespace System.Validations;

public class ValidatableValidation
    : Validation<IValidatable, IValidatableValidation>,
        IValidatableValidation {

    public ValidatableValidation(IValidatable? subject, string? source, IEnumerable<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IFinishesValidation Valid() {
        if (Subject is null) return this;
        foreach (var error in Subject.Validate().Errors) {
            error.Arguments[0] = $"{Source}.{error.Arguments[0]}";
            Errors.Add(error);
        }

        return new ObjectValidation(Subject, Source, Errors);
    }
}
