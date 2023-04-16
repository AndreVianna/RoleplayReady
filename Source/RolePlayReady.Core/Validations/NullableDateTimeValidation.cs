namespace System.Validations;

public class NullableDateTimeValidation
    : Validation<DateTime?, IDateTimeValidation>,
      INullableDateTimeValidation {

    public NullableDateTimeValidation(DateTime? subject, string? source, ICollection<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IDateTimeValidation NotNull() {
        if (Subject is null) Errors.Add(new(CannotBeNull, Source));
        return new DateTimeValidation(Subject, Source, Errors);
    }
}