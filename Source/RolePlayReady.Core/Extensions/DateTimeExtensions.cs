using System.Validation.Builder;

namespace System.Extensions;

public static class DateTimeExtensions {
    public static IConnectors<DateTime?, DateTimeValidators> IsRequired(this DateTime subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => DateTimeValidators.CreateAsRequired(subject, source!).AsConnection<DateTime?, DateTimeValidators>();
    public static IConnectors<DateTime?, DateTimeValidators> IsOptional(this DateTime? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => DateTimeValidators.CreateAsOptional(subject, source!).AsConnection<DateTime?, DateTimeValidators>();
    public static IConnectors<DateTime?, DateTimeValidators> IsRequired(this DateTime? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => DateTimeValidators.CreateAsRequired(subject, source!).AsConnection<DateTime?, DateTimeValidators>();
}