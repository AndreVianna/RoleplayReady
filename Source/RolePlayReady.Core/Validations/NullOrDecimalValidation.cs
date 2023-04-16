namespace System.Validations;

public class NullOrDecimalValidation
    : Validation<decimal?, ReferenceTypeValidation, IReferenceTypeValidations>,
      INullOrDecimalValidations {

    public NullOrDecimalValidation(decimal? subject, string? source, ICollection<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IConnectors<IDecimalValidations> NotNull() {
        if (Subject is null) Errors.Add(new(CannotBeNull, Source));
        return new DecimalValidation(Subject, Source, Errors);
    }
}