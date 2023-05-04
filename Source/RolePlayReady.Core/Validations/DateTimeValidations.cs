namespace System.Validations;

public class DateTimeValidations
    : Validations<DateTime?, DateTimeValidations>
        , IDateTimeValidations {
    public static DateTimeValidations CreateAsOptional(DateTime? subject, string source)
        => new(subject, source);
    public static DateTimeValidations CreateAsRequired(DateTime? subject, string source)
        => new(subject, source, EnsureNotNull(subject, source));

    private DateTimeValidations(DateTime? subject, string source, IEnumerable<ValidationError>? previousErrors = null)
        : base(ValidationMode.None, subject, source, previousErrors) {
        Connector = new ValidationsConnector<DateTime?, DateTimeValidations>(Subject, this);
    }

    public IValidationsConnector<DateTime?, DateTimeValidations> IsBefore(DateTime threshold) {
        if (Subject >= threshold)
            Errors.Add(new(MustBeBefore, Source, threshold, Subject));

        return Connector;
    }

    public IValidationsConnector<DateTime?, DateTimeValidations> StartsOn(DateTime threshold) {
        if (Subject < threshold)
            Errors.Add(new(CannotBeBefore, Source, threshold, Subject));

        return Connector;
    }

    public IValidationsConnector<DateTime?, DateTimeValidations> EndsOn(DateTime threshold) {
        if (Subject > threshold)
            Errors.Add(new(CannotBeAfter, Source, threshold, Subject));

        return Connector;
    }

    public IValidationsConnector<DateTime?, DateTimeValidations> IsAfter(DateTime threshold) {
        if (Subject <= threshold)
            Errors.Add(new(MustBeAfter, Source, threshold, Subject));

        return Connector;
    }
}