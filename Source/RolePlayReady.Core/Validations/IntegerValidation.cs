namespace System.Validations;

public class IntegerValidation
    : Validation<int?, IntegerValidation, IIntegerValidations>,
      IIntegerValidations {

    public IntegerValidation(int? subject, string? source, ICollection<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    // Equivalent to number >= lowerLimit
    public IConnectors<IIntegerValidations> GreaterOrEqualTo(int lowerLimit) {
        if (Subject is not IComparable<int> subject) return this;
        if (subject.CompareTo(lowerLimit) < 0)
            Errors.Add(new(CannotBeLessThan, Source, lowerLimit, subject));
        return this;
    }

    // Equivalent to number < upperLimit
    public IConnectors<IIntegerValidations> LessThan(int upperLimit) {
        if (Subject is not IComparable<int> subject)
            return this;
        if (subject.CompareTo(upperLimit) >= 0)
            Errors.Add(new(CannotBeGreaterOrEqualTo, Source, upperLimit, subject));
        return this;
    }

    // Equivalent to number > lowerLimit
    public IConnectors<IIntegerValidations> GreaterThan(int lowerLimit) {
        if (Subject is not IComparable<int> subject)
            return this;
        if (subject.CompareTo(lowerLimit) <= 0)
            Errors.Add(new(CannotBeLessOrEqualTo, Source, lowerLimit, subject));
        return this;
    }

    // Equivalent to number <= upperLimit
    public IConnectors<IIntegerValidations> LessOrEqualTo(int upperLimit) {
        if (Subject is not IComparable<int> subject)
            return this;
        if (subject.CompareTo(upperLimit) > 0)
            Errors.Add(new(CannotBeGreaterThan, Source, upperLimit, subject));
        return this;
    }
}