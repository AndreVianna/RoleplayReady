namespace System.Validations;

public class NumberValidation<TValue>
    : Validation<IComparable<TValue>?, INumberValidation<TValue>>,
      INumberValidation<TValue> where TValue : IComparable<TValue> {

    public NumberValidation(IComparable<TValue>? subject, string? source, IEnumerable<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IConnectsToOrFinishes<INumberValidation<TValue>> GreaterThan(TValue minimum) {
        if (Subject is null)
            return this;
        if (Subject.CompareTo(minimum) <= 0)
            Errors.Add(new(CannotBeLessOrEqualTo, Source, minimum, Subject));
        return this;
    }

    public IConnectsToOrFinishes<INumberValidation<TValue>> LessThan(TValue maximum) {
        if (Subject is null)
            return this;
        if (Subject.CompareTo(maximum) >= 0)
            Errors.Add(new(CannotBeGreaterOrEqualTo, Source, maximum, Subject));
        return this;
    }

    public IConnectsToOrFinishes<INumberValidation<TValue>> GreaterOrEqualTo(TValue minimum) {
        if (Subject is null)
            return this;
        if (Subject.CompareTo(minimum) < 0)
            Errors.Add(new(CannotBeLessThan, Source, minimum, Subject));
        return this;
    }

    public IConnectsToOrFinishes<INumberValidation<TValue>> LessOrEqualTo(TValue maximum) {
        if (Subject is null)
            return this;
        if (Subject.CompareTo(maximum) > 0)
            Errors.Add(new(CannotBeGreaterThan, Source, maximum, Subject));
        return this;
    }
}