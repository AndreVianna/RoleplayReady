namespace System.Validations;

public class NullableDecimalValidation
    : Validation<decimal?, IDecimalValidation>,
      INullableDecimalValidation {

    public NullableDecimalValidation(decimal? subject, string? source, ICollection<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IDecimalValidation NotNull() {
        if (Subject is null) Errors.Add(new(CannotBeNull, Source));
        return new DecimalValidation(Subject, Source, Errors);
    }
}