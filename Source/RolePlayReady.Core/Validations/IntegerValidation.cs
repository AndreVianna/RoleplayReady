namespace System.Validations;

public class IntegerValidation
    : Validation<IComparable<int>?, IIntegerValidation>,
      IIntegerValidation {

    public IntegerValidation(IComparable<int>? subject, string? source, IEnumerable<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IConnectsToOrFinishes<IIntegerValidation> GreaterThan(int minimum) {
        if (Subject is null) return this;
        if (Subject.CompareTo(minimum) <= 0)
            Errors.Add(new(CannotBeLessOrEqualTo, Source, minimum, Subject));
        return this;
    }

    public IConnectsToOrFinishes<IIntegerValidation> LessThan(int maximum) {
        if (Subject is null) return this;
        if (Subject.CompareTo(maximum) >= 0)
            Errors.Add(new(CannotBeGreaterOrEqualTo, Source, maximum, Subject));
        return this;
    }

    public IConnectsToOrFinishes<IIntegerValidation> GreaterOrEqualTo(int minimum) {
        if (Subject is null) return this;
        if (Subject.CompareTo(minimum) < 0)
            Errors.Add(new(CannotBeLessThan, Source, minimum, Subject));
        return this;
    }

    public IConnectsToOrFinishes<IIntegerValidation> LessOrEqualTo(int maximum) {
        if (Subject is null) return this;
        if (Subject.CompareTo(maximum) > 0)
            Errors.Add(new(CannotBeGreaterThan, Source, maximum, Subject));
        return this;
    }
}