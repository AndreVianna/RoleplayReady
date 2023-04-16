namespace System.Validations;

public class ReferenceTypeValidation
    : Validation<object, IReferenceTypeValidation>,
      IReferenceTypeValidation {

    public ReferenceTypeValidation(object? subject, string? source, ICollection<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IReferenceTypeValidation NotNull() {
        if (Subject is null)
            Errors.Add(new(CannotBeNull, Source));
        return this;
    }
}