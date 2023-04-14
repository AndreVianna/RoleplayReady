namespace System.Validators;

public class DateTimeValidator
    : Validator<DateTime?, IDateTimeChecks, IDateTimeConnectors>,
        IDateTimeChecks,
        IDateTimeConnectors {

    public DateTimeValidator(DateTime? subject, string? source)
        : base(subject, source) {
    }
}