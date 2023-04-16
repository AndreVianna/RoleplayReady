namespace System.Validations;

public class ValidatableValidation
    : Validation<IValidatable, IValidatableTypeValidation>,
        IValidatableTypeValidation {

    public ValidatableValidation(IValidatable? subject, string? source, ICollection<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IValidatableTypeValidation NotNull() {
        if (Subject is null) 
            Errors.Add(new(CannotBeNull, Source));
        return this;
    }

    public IFinishesValidation Valid() {
        if (Subject is null) return new ReferenceTypeValidation(Subject, Source, Errors);
        foreach (var error in Subject.Validate().Errors) {
            error.Arguments[0] = $"{Source}.{error.Arguments[0]}";
            Errors.Add(error);
        }

        return new ReferenceTypeValidation(Subject, Source, Errors);
    }
}
