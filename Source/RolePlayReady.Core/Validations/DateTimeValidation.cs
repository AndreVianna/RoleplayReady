namespace System.Validations;

public class DateTimeValidation
    : Validation<DateTime?, DateTimeValidation, IDateTimeValidations>,
      IDateTimeValidations {

    public DateTimeValidation(DateTime? subject, string? source, ICollection<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    // Equivalent to dateTime > reference
    public IConnectors<IDateTimeValidations> After(DateTime reference) {
        if (Subject is not IComparable<DateTime> subject) return this;
        if (subject.CompareTo(reference) <= 0)
            Errors.Add(new(CannotBeAtOrBefore, Source, reference, subject));
        return this;
    }

    // Equivalent to dateTime < reference
    public IConnectors<IDateTimeValidations> Before(DateTime reference) {
        if (Subject is not IComparable<DateTime> subject) return this;
        if (subject.CompareTo(reference) >= 0)
            Errors.Add(new(CannotBeAtOrAfter, Source, reference, subject));
        return this;
    }

    // Equivalent to dateTime >= reference
    public IConnectors<IDateTimeValidations> AtOrAftter(DateTime reference) {
        if (Subject is not IComparable<DateTime> subject) return this;
        if (subject.CompareTo(reference) < 0)
            Errors.Add(new(CannotBeBefore, Source, reference, subject));
        return this;
    }

    // Equivalent to dateTime <= reference
    public IConnectors<IDateTimeValidations> AtOrBefore(DateTime reference) {
        if (Subject is not IComparable<DateTime> subject) return this;
        if (subject.CompareTo(reference) > 0)
            Errors.Add(new(CannotBeAfter, Source, reference, subject));
        return this;
    }
}