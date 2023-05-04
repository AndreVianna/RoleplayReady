namespace System.Validations.Extensions;

public static class DateTimeExtensions {
    public static IValidationsConnector<DateTime, DateTimeValidations> IsRequired(this DateTime subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => DateTimeValidations.CreateAsRequired(subject, source!).AsConnection<DateTime, DateTimeValidations>();
    public static IValidationsConnector<DateTime?, DateTimeValidations> IsOptional(this DateTime? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => DateTimeValidations.CreateAsOptional(subject, source!).AsConnection<DateTime?, DateTimeValidations>();
    public static IValidationsConnector<DateTime?, DateTimeValidations> IsRequired(this DateTime? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => DateTimeValidations.CreateAsRequired(subject, source!).AsConnection<DateTime?, DateTimeValidations>();
}