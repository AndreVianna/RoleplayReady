namespace System.Validations;

public class NumberValidation<TValue>
    : Validation<IComparable<TValue>?, INumberValidation<TValue>>,
      INumberValidation<TValue> where TValue : IComparable<TValue> {

    public NumberValidation(IComparable<TValue>? subject, string? source, IEnumerable<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IConnectsToOrFinishes<INumberValidation<TValue>> GreaterThan(TValue threshold) {
        if (Subject is null) return this;
        if (Subject.CompareTo(threshold) <= 0)
            Errors.Add(new(MustBeGraterThan, Source, threshold, Subject));
        return this;
    }

    public IConnectsToOrFinishes<INumberValidation<TValue>> LessThan(TValue threshold) {
        if (Subject is null) return this;
        if (Subject.CompareTo(threshold) >= 0)
            Errors.Add(new(MustBeLessThan, Source, threshold, Subject));
        return this;
    }

    public IConnectsToOrFinishes<INumberValidation<TValue>> GreaterOrEqualTo(TValue threshold) {
        if (Subject is null) return this;
        if (Subject.CompareTo(threshold) < 0)
            Errors.Add(new(CannotBeLessThan, Source, threshold, Subject));
        return this;
    }

    public IConnectsToOrFinishes<INumberValidation<TValue>> LessOrEqualTo(TValue threshold) {
        if (Subject is null) return this;
        if (Subject.CompareTo(threshold) > 0)
            Errors.Add(new(CannotBeGreaterThan, Source, threshold, Subject));
        return this;
    }
}