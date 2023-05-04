namespace System.Validations;

public class DecimalValidations<TValue>
    : Validations<IComparable<TValue>, NumberValidations<TValue>>
        , INumberValidations<TValue>
    where TValue : IComparable<TValue> {

    public static NumberValidations<TSubject> CreateAsOptional<TSubject>(TSubject? subject, string source)
        where TSubject : IComparable<TSubject>
        => new(subject, source);
    public static NumberValidations<TSubject> CreateAsRequired<TSubject>(TSubject? subject, string source)
        where TSubject : IComparable<TSubject>
        => new(subject, source, EnsureNotNull(subject, source));

    private NumberValidations(TValue? subject, string source, IEnumerable<ValidationError>? previousErrors = null)
        : base(ValidationMode.None, subject, source, previousErrors) {
        Connector = new ValidationsConnector<IComparable<TValue>, NumberValidations<TValue>>(Subject, this);
    }

    public IValidationsConnector<IComparable<TValue>, NumberValidations<TValue>> MinimumIs(TValue threshold) {
        if (Subject is not null && Subject.CompareTo(threshold) < 0)
            Errors.Add(new(CannotBeLessThan, Source, threshold, Subject));

        return Connector;
    }

    public IValidationsConnector<IComparable<TValue>, NumberValidations<TValue>> IsGreaterThan(TValue threshold) {
        if (Subject is not null && Subject.CompareTo(threshold) <= 0)
            Errors.Add(new(MustBeGraterThan, Source, threshold, Subject));

        return Connector;
    }

    public IValidationsConnector<IComparable<TValue>, NumberValidations<TValue>> IsEqualTo(TValue threshold) {
        if (Subject is not null && Subject.CompareTo(threshold) != 0)
            Errors.Add(new(MustBeEqualTo, Source, threshold, Subject));

        return Connector;
    }

    public IValidationsConnector<IComparable<TValue>, NumberValidations<TValue>> IsLessThan(TValue threshold) {
        if (Subject is not null && Subject.CompareTo(threshold) >= 0)
            Errors.Add(new(MustBeLessThan, Source, threshold, Subject));

        return Connector;
    }

    public IValidationsConnector<IComparable<TValue>, NumberValidations<TValue>> MaximumIs(TValue threshold) {
        if (Subject is not null && Subject.CompareTo(threshold) > 0)
            Errors.Add(new(CannotBeGreaterThan, Source, threshold, Subject));

        return Connector;
    }
}