namespace System.Validations;

public class NullOrDateTimeValidation
    : Validation<DateTime?, NullOrDateTimeValidation, IReferenceTypeValidations>,
      INullOrDateTimeValidations {

    public NullOrDateTimeValidation(DateTime? subject, string? source, ICollection<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IConnectors<IDateTimeValidations> NotNull() {
        if (Subject is null) Errors.Add(new(CannotBeNull, Source));
        return new DateTimeValidation(Subject, Source, Errors);
    }
}