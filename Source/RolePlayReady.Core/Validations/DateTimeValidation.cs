namespace System.Validations;

public class DateTimeValidation
    : Validation<IComparable<DateTime>?, IDateTimeValidation>,
      IDateTimeValidation {

    public DateTimeValidation(IComparable<DateTime>? subject, string? source, IEnumerable<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    // Equivalent to dateTime > reference
    public IConnectsToOrFinishes<IDateTimeValidation> After(DateTime reference) {
        if (Subject is null) return this;
        if (Subject.CompareTo(reference) <= 0)
            Errors.Add(new(CannotBeAtOrBefore, Source, reference, Subject));
        return this;
    }

    // Equivalent to dateTime < reference
    public IConnectsToOrFinishes<IDateTimeValidation> Before(DateTime reference) {
        if (Subject is null) return this;
        if (Subject.CompareTo(reference) >= 0)
            Errors.Add(new(CannotBeAtOrAfter, Source, reference, Subject));
        return this;
    }

    // Equivalent to dateTime >= reference
    public IConnectsToOrFinishes<IDateTimeValidation> StartsOn(DateTime reference) {
        if (Subject is null) return this;
        if (Subject.CompareTo(reference) < 0)
            Errors.Add(new(CannotBeBefore, Source, reference, Subject));
        return this;
    }

    // Equivalent to dateTime <= reference
    public IConnectsToOrFinishes<IDateTimeValidation> EndsOn(DateTime reference) {
        if (Subject is null) return this;
        if (Subject.CompareTo(reference) > 0)
            Errors.Add(new(CannotBeAfter, Source, reference, Subject));
        return this;
    }
}