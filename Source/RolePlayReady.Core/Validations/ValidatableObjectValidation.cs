namespace System.Validations;

public class ValidatableObjectValidation
    : Validation<IValidatable, ValidatableObjectValidation, IValidatableObjectValidations>,
        IValidatableObjectValidations {

    public ValidatableObjectValidation(IValidatable? subject, string? source, ICollection<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IConnectors<IValidatableObjectValidations> NotNull() {
        if (Subject is null) 
            Errors.Add(new(CannotBeNull, Source));
        return this;
    }

    public IConnectors<IReferenceTypeValidations> Valid() {
        if (Subject is null) return new ReferenceTypeValidation(Subject, Source, Errors);
        foreach (var error in Subject.Validate().Errors) {
            error.Arguments[0] = $"{Source}.{error.Arguments[0]}";
            Errors.Add(error);
        }

        return new ReferenceTypeValidation(Subject, Source, Errors);
    }
}
