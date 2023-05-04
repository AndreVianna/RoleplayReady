namespace System.Validations;

public class DateTimeValidations : IDateTimeValidations {
    private readonly Connects<DateTimeValidations> _connector;

    public DateTimeValidations(DateTime? subject, string source, IEnumerable<ValidationError>? previousErrors = null) {
        Subject = subject;
        Source = source;
        Errors = previousErrors?.ToList() ?? new List<ValidationError>();
        _connector = new Connects<DateTimeValidations>(this);
    }

    public DateTime? Subject { get; }
    public string Source { get; }
    public List<ValidationError> Errors { get; }

    public IConnects<IDateTimeValidations> IsBefore(DateTime threshold) {
        if (Subject >= threshold)
            Errors.Add(new(MustBeBefore, Source, threshold, Subject));

        return _connector;
    }

    public IConnects<IDateTimeValidations> StartsOn(DateTime threshold) {
        if (Subject < threshold)
            Errors.Add(new(CannotBeBefore, Source, threshold, Subject));

        return _connector;
    }

    public IConnects<IDateTimeValidations> EndsOn(DateTime threshold) {
        if (Subject > threshold)
            Errors.Add(new(CannotBeAfter, Source, threshold, Subject));

        return _connector;
    }

    public IConnects<IDateTimeValidations> IsAfter(DateTime threshold) {
        if (Subject <= threshold)
            Errors.Add(new(MustBeAfter, Source, threshold, Subject));

        return _connector;
    }
}