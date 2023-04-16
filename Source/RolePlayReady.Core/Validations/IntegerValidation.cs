namespace System.Validations;

public class IntegerValidation
    : Validation<int?, IIntegerValidation>,
      IIntegerValidation {

    public IntegerValidation(int? subject, string? source, ICollection<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IIntegerValidation GreaterThan(int minimum) {
        if (Subject is null) return this;
        if (Subject.Value.CompareTo(minimum) <= 0)
            Errors.Add(new(CannotBeLessOrEqualTo, Source, minimum, Subject.Value));
        return this;
    }

    public IIntegerValidation LessThan(int maximum) {
        if (Subject is null) return this;
        if (Subject.Value.CompareTo(maximum) >= 0)
            Errors.Add(new(CannotBeGreaterOrEqualTo, Source, maximum, Subject.Value));
        return this;
    }

    public IIntegerValidation GreaterOrEqualTo(int minimum) {
        if (Subject is null) return this;
        if (Subject.Value.CompareTo(minimum) < 0)
            Errors.Add(new(CannotBeLessThan, Source, minimum, Subject.Value));
        return this;
    }

    public IIntegerValidation LessOrEqualTo(int maximum) {
        if (Subject is null) return this;
        if (Subject.Value.CompareTo(maximum) > 0)
            Errors.Add(new(CannotBeGreaterThan, Source, maximum, Subject.Value));
        return this;
    }
}