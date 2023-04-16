namespace System.Validations;

public class DecimalValidation
    : Validation<decimal?, DecimalValidation, IDecimalValidations>,
      IDecimalValidations {

    public DecimalValidation(decimal? subject, string? source, ICollection<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    // Equivalent to number >= lowerLimit
    public IConnectors<IDecimalValidations> GreaterOrEqualTo(decimal lowerLimit) {
        if (Subject is not IComparable<decimal> subject) return this;
        if (subject.CompareTo(lowerLimit) < 0)
            Errors.Add(new(CannotBeLessThan, Source, lowerLimit, subject));
        return this;
    }

    // Equivalent to number < upperLimit
    public IConnectors<IDecimalValidations> LessThan(decimal upperLimit) {
        if (Subject is not IComparable<decimal> subject) return this;
        if (subject.CompareTo(upperLimit) >= 0)
            Errors.Add(new(CannotBeGreaterOrEqualTo, Source, upperLimit, subject));
        return this;
    }

    // Equivalent to number > lowerLimit
    public IConnectors<IDecimalValidations> GreaterThan(decimal lowerLimit) {
        if (Subject is not IComparable<decimal> subject) return this;
        if (subject.CompareTo(lowerLimit) <= 0)
            Errors.Add(new(CannotBeLessOrEqualTo, Source, lowerLimit, subject));
        return this;
    }

    // Equivalent to number <= upperLimit
    public IConnectors<IDecimalValidations> LessOrEqualtTo(decimal upperLimit) {
        if (Subject is not IComparable<decimal> subject) return this;
        if (subject.CompareTo(upperLimit) > 0)
            Errors.Add(new(CannotBeGreaterThan, Source, upperLimit, subject));
        return this;
    }
}