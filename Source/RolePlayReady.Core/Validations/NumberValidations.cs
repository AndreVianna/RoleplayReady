namespace System.Validations;

public class NumberValidations<TValue> : INumberValidations<TValue> {
    private readonly Connects<NumberValidations<TValue>> _connector;

    public NumberValidations(IComparable<TValue>? subject, string source, IEnumerable<ValidationError>? previousErrors = null)  {
        Subject = subject;
        Source = source;
        Errors = previousErrors?.ToList() ?? new List<ValidationError>();
        _connector = new Connects<NumberValidations<TValue>>(this);
    }

    public IComparable<TValue>? Subject { get; }
    public string Source { get; }
    public List<ValidationError> Errors { get; }

    public IConnects<INumberValidations<TValue>> MinimumIs(TValue threshold) {
        if (Subject is not null && Subject.CompareTo(threshold) < 0)
            Errors.Add(new(CannotBeLessThan, Source, threshold, Subject));

        return _connector;
    }

    public IConnects<INumberValidations<TValue>> IsGreaterThan(TValue threshold) {
        if (Subject is not null && Subject.CompareTo(threshold) <= 0)
            Errors.Add(new(MustBeGraterThan, Source, threshold, Subject));

        return _connector;
    }

    public IConnects<INumberValidations<TValue>> IsEqualTo(TValue threshold) {
        if (Subject is not null && Subject.CompareTo(threshold) != 0)
            Errors.Add(new(MustBeEqualTo, Source, threshold, Subject));

        return _connector;
    }

    public IConnects<INumberValidations<TValue>> IsLessThan(TValue threshold) {
        if (Subject is not null && Subject.CompareTo(threshold) >= 0)
            Errors.Add(new(MustBeLessThan, Source, threshold, Subject));

        return _connector;
    }

    public IConnects<INumberValidations<TValue>> MaximumIs(TValue threshold) {
        if (Subject is not null && Subject.CompareTo(threshold) > 0)
            Errors.Add(new(CannotBeGreaterThan, Source, threshold, Subject));

        return _connector;
    }
}