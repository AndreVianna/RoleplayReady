namespace System.Validations;

public class DateTimeValidation
    : Validation<DateTime?, IDateTimeValidation>,
      IDateTimeValidation {

    public DateTimeValidation(DateTime? subject, string? source, ICollection<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    // Equivalent to dateTime > reference
    public IDateTimeValidation After(DateTime reference) {
        if (Subject is not IComparable<DateTime> subject) return this;
        if (subject.CompareTo(reference) <= 0)
            Errors.Add(new(CannotBeAtOrBefore, Source, reference, subject));
        return this;
    }

    // Equivalent to dateTime < reference
    public IDateTimeValidation Before(DateTime reference) {
        if (Subject is not IComparable<DateTime> subject) return this;
        if (subject.CompareTo(reference) >= 0)
            Errors.Add(new(CannotBeAtOrAfter, Source, reference, subject));
        return this;
    }

    // Equivalent to dateTime >= reference
    public IDateTimeValidation StartsOn(DateTime reference) {
        if (Subject is not IComparable<DateTime> subject) return this;
        if (subject.CompareTo(reference) < 0)
            Errors.Add(new(CannotBeBefore, Source, reference, subject));
        return this;
    }

    // Equivalent to dateTime <= reference
    public IDateTimeValidation EndsOn(DateTime reference) {
        if (Subject is not IComparable<DateTime> subject) return this;
        if (subject.CompareTo(reference) > 0)
            Errors.Add(new(CannotBeAfter, Source, reference, subject));
        return this;
    }
}