namespace System.Validations;

public class DecimalValidation
    : Validation<IComparable<decimal>?, IDecimalValidation>,
      IDecimalValidation {

    public DecimalValidation(IComparable<decimal>? subject, string? source, IEnumerable<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IConnectsToOrFinishes<IDecimalValidation> GreaterThan(decimal minimum) {
        if (Subject is null) return this;
        if (Subject.CompareTo(minimum) <= 0)
            Errors.Add(new(CannotBeLessOrEqualTo, Source, minimum, Subject));
        return this;
    }

    public IConnectsToOrFinishes<IDecimalValidation> LessThan(decimal maximum) {
        if (Subject is null) return this;
        if (Subject.CompareTo(maximum) >= 0)
            Errors.Add(new(CannotBeGreaterOrEqualTo, Source, maximum, Subject));
        return this;
    }

    public IConnectsToOrFinishes<IDecimalValidation> GreaterOrEqualTo(decimal minimum) {
        if (Subject is null) return this;
        if (Subject.CompareTo(minimum) < 0)
            Errors.Add(new(CannotBeLessThan, Source, minimum, Subject));
        return this;
    }

    public IConnectsToOrFinishes<IDecimalValidation> LessOrEqualTo(decimal maximum) {
        if (Subject is null) return this;
        if (Subject.CompareTo(maximum) > 0)
            Errors.Add(new(CannotBeGreaterThan, Source, maximum, Subject));
        return this;
    }
}