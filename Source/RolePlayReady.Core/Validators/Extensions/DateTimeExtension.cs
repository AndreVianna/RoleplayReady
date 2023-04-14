namespace System.Validators.Extensions;

public static class DateTimeExtension {
    public static IDateTimeChecks ValueIs(this DateTime? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new DateTimeValidator(subject, source);

    public static IDateTimeChecks ValueIs(this DateTime subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new DateTimeValidator(subject, source);
}