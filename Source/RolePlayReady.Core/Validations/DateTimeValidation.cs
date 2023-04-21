namespace System.Validations;

public class DateTimeValidation
    : Validation<IComparable<DateTime>?, IDateTimeValidators>,
      IDateTimeValidation {
    public DateTimeValidation(IComparable<DateTime>? subject, string? source, IEnumerable<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    // Equivalent to dateTime < threshold
    public IConnectsToOrFinishes<IDateTimeValidators> IsBefore(DateTime threshold) {
        if (Subject is null)
            return this;
        if (Subject.CompareTo(threshold) >= 0)
            Errors.Add(new(MustBeBefore, Source, threshold, Subject));
        return this;
    }

    // Equivalent to dateTime >= threshold
    public IConnectsToOrFinishes<IDateTimeValidators> StartsOn(DateTime threshold) {
        if (Subject is null)
            return this;
        if (Subject.CompareTo(threshold) < 0)
            Errors.Add(new(CannotBeBefore, Source, threshold, Subject));
        return this;
    }

    // Equivalent to dateTime <= threshold
    public IConnectsToOrFinishes<IDateTimeValidators> EndsOn(DateTime threshold) {
        if (Subject is null)
            return this;
        if (Subject.CompareTo(threshold) > 0)
            Errors.Add(new(CannotBeAfter, Source, threshold, Subject));
        return this;
    }

    // Equivalent to dateTime > threshold
    public IConnectsToOrFinishes<IDateTimeValidators> IsAfter(DateTime threshold) {
        if (Subject is null)
            return this;
        if (Subject.CompareTo(threshold) <= 0)
            Errors.Add(new(MustBeAfter, Source, threshold, Subject));
        return this;
    }
}