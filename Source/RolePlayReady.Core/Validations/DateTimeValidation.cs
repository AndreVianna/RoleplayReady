namespace System.Validations;

public class DateTimeValidation
    : Validation<IComparable<DateTime>?, IDateTimeValidation>,
      IDateTimeValidation {

    public DateTimeValidation(IComparable<DateTime>? subject, string? source, IEnumerable<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    // Equivalent to dateTime > threshold
    public IConnectsToOrFinishes<IDateTimeValidation> After(DateTime threshold) {
        if (Subject is null) return this;
        if (Subject.CompareTo(threshold) <= 0)
            Errors.Add(new(MustBeAfter, Source, threshold, Subject));
        return this;
    }

    // Equivalent to dateTime < threshold
    public IConnectsToOrFinishes<IDateTimeValidation> Before(DateTime threshold) {
        if (Subject is null) return this;
        if (Subject.CompareTo(threshold) >= 0)
            Errors.Add(new(MustBeBefore, Source, threshold, Subject));
        return this;
    }

    // Equivalent to dateTime >= threshold
    public IConnectsToOrFinishes<IDateTimeValidation> StartsOn(DateTime threshold) {
        if (Subject is null) return this;
        if (Subject.CompareTo(threshold) < 0)
            Errors.Add(new(CannotBeBefore, Source, threshold, Subject));
        return this;
    }

    // Equivalent to dateTime <= threshold
    public IConnectsToOrFinishes<IDateTimeValidation> EndsOn(DateTime threshold) {
        if (Subject is null) return this;
        if (Subject.CompareTo(threshold) > 0)
            Errors.Add(new(CannotBeAfter, Source, threshold, Subject));
        return this;
    }
}