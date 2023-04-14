namespace System.Validators.Extensions;

public static class DateTimeExtension
{
    public static IDateTimeChecks Is(this DateTime? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new DateTimeValidator(subject, source);
}