using System.Validation;
using System.Validation.Abstractions;

namespace System.Extensions;

public static class DecimalExtensions {
    public static IValidatorsConnector<decimal?, DecimalValidators> IsRequired(this decimal subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => DecimalValidators.CreateAsRequired(subject, source!).AsConnection<decimal?, DecimalValidators>();
    public static IValidatorsConnector<decimal?, DecimalValidators> IsOptional(this decimal? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => DecimalValidators.CreateAsOptional(subject, source!).AsConnection<decimal?, DecimalValidators>();
    public static IValidatorsConnector<decimal?, DecimalValidators> IsRequired(this decimal? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => DecimalValidators.CreateAsRequired(subject, source!).AsConnection<decimal?, DecimalValidators>();
}