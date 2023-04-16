namespace System.Validations;

public class NullOrIntegerValidation
    : Validation<int?, ReferenceTypeValidation, IReferenceTypeValidations>,
      INullOrIntegerValidations {

    public NullOrIntegerValidation(int? subject, string? source, ICollection<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IConnectors<IIntegerValidations> NotNull() {
        if (Subject is null) Errors.Add(new(CannotBeNull, Source));
        return new IntegerValidation(Subject, Source, Errors);
    }
}