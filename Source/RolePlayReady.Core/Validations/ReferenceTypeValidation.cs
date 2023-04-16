namespace System.Validations;

public class ReferenceTypeValidation
    : Validation<object, ReferenceTypeValidation, IReferenceTypeValidations>,
      IReferenceTypeValidations {

    public ReferenceTypeValidation(object? subject, string? source, ICollection<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IConnectors<IReferenceTypeValidations> NotNull() {
        if (Subject is null)
            Errors.Add(new(CannotBeNull, Source));
        return this;
    }
}