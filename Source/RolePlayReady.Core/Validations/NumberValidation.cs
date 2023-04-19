namespace System.Validations;

public class NumberValidation<TValue>
    : Validation<IComparable<TValue>?, INumberValidators<TValue>>,
      INumberValidation<TValue> where TValue : IComparable<TValue> {

    public NumberValidation(IComparable<TValue>? subject, string? source, IEnumerable<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IConnectsToOrFinishes<INumberValidators<TValue>> MinimumIs(TValue threshold) {
        if (Subject is null) return this;
        if (Subject.CompareTo(threshold) < 0)
            Errors.Add(new(CannotBeLessThan, Source, threshold, Subject));
        return this;
    }

    public IConnectsToOrFinishes<INumberValidators<TValue>> IsGreaterThan(TValue threshold) {
        if (Subject is null) return this;
        if (Subject.CompareTo(threshold) <= 0)
            Errors.Add(new(MustBeGraterThan, Source, threshold, Subject));
        return this;
    }

    public IConnectsToOrFinishes<INumberValidators<TValue>> IsEqualTo(TValue threshold) {
        if (Subject is null) return this;
        if (Subject.CompareTo(threshold) != 0)
            Errors.Add(new(MustBeEqualTo, Source, threshold, Subject));
        return this;
    }

    public IConnectsToOrFinishes<INumberValidators<TValue>> IsLessThan(TValue threshold) {
        if (Subject is null) return this;
        if (Subject.CompareTo(threshold) >= 0)
            Errors.Add(new(MustBeLessThan, Source, threshold, Subject));
        return this;
    }

    public IConnectsToOrFinishes<INumberValidators<TValue>> MaximumIs(TValue threshold) {
        if (Subject is null) return this;
        if (Subject.CompareTo(threshold) > 0)
            Errors.Add(new(CannotBeGreaterThan, Source, threshold, Subject));
        return this;
    }
}