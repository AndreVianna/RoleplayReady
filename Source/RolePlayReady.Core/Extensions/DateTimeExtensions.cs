using System.Validation;
using System.Validation.Abstractions;

namespace System.Extensions;

public static class DateTimeExtensions {
    public static IValidatorsConnector<DateTime?, DateTimeValidators> IsRequired(this DateTime subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => DateTimeValidators.CreateAsRequired(subject, source!).AsConnection<DateTime?, DateTimeValidators>();
    public static IValidatorsConnector<DateTime?, DateTimeValidators> IsOptional(this DateTime? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => DateTimeValidators.CreateAsOptional(subject, source!).AsConnection<DateTime?, DateTimeValidators>();
    public static IValidatorsConnector<DateTime?, DateTimeValidators> IsRequired(this DateTime? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => DateTimeValidators.CreateAsRequired(subject, source!).AsConnection<DateTime?, DateTimeValidators>();
}