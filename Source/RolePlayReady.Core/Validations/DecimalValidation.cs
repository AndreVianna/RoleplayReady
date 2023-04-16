namespace System.Validations;

public class DecimalValidation
    : Validation<decimal?, IDecimalValidation>,
      IDecimalValidation {

    public DecimalValidation(decimal? subject, string? source, ICollection<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IDecimalValidation GreaterThan(decimal minimum) {
        if (Subject is null) return this;
        if (Subject.Value.CompareTo(minimum) <= 0)
            Errors.Add(new(CannotBeLessOrEqualTo, Source, minimum, Subject.Value));
        return this;
    }

    public IDecimalValidation LessThan(decimal maximum) {
        if (Subject is null) return this;
        if (Subject.Value.CompareTo(maximum) >= 0)
            Errors.Add(new(CannotBeGreaterOrEqualTo, Source, maximum, Subject.Value));
        return this;
    }

    public IDecimalValidation GreaterOrEqualTo(decimal minimum) {
        if (Subject is null) return this;
        if (Subject.Value.CompareTo(minimum) < 0)
            Errors.Add(new(CannotBeLessThan, Source, minimum, Subject.Value));
        return this;
    }

    public IDecimalValidation LessOrEqualTo(decimal maximum) {
        if (Subject is null) return this;
        if (Subject.Value.CompareTo(maximum) > 0)
            Errors.Add(new(CannotBeGreaterThan, Source, maximum, Subject.Value));
        return this;
    }
}