using System.Validation;
using System.Validation.Abstractions;

namespace System.Extensions;

public static class IntegerExtensions {
    public static IValidatorsConnector<int?, IntegerValidators> IsRequired(this int subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => IntegerValidators.CreateAsRequired(subject, source!).AsConnection<int?, IntegerValidators>();
    public static IValidatorsConnector<int?, IntegerValidators> IsOptional(this int? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => IntegerValidators.CreateAsOptional(subject, source!).AsConnection<int?, IntegerValidators>();
    public static IValidatorsConnector<int?, IntegerValidators> IsRequired(this int? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => IntegerValidators.CreateAsRequired(subject, source!).AsConnection<int?, IntegerValidators>();
}