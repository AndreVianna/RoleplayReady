namespace System.Validations.Extensions;

public static class DateTimeExtensions {
    public static IDateTimeValidations Value(this DateTime subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new DateTimeValidations(subject, source!);
    public static IDateTimeValidations IsNullOr(this DateTime? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new DateTimeValidations(subject, source!);
    public static IConnects<IDateTimeValidations> IsNotNull(this DateTime? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new Connects<IDateTimeValidations>(new DateTimeValidations(subject, source!, Validation.EnsureNotNull(subject, source!)));
}