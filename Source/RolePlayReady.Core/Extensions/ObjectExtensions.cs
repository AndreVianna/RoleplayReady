using System.Validation;
using System.Validation.Abstractions;

namespace System.Extensions;

public static class ObjectExtensions {
    public static IValidatorsConnector<object?, ObjectValidators> IsNotNull(this object? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => ObjectValidators.CreateAsRequired(subject, source!).AsConnection<object?, ObjectValidators>();
    public static IValidatorsConnector<IValidatable?, ValidatableValidators> IsOptional(this IValidatable? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => ValidatableValidators.CreateAsOptional(subject, source!).AsConnection<IValidatable?, ValidatableValidators>();
    public static IValidatorsConnector<IValidatable?, ValidatableValidators> IsRequired(this IValidatable? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => ValidatableValidators.CreateAsRequired(subject, source!).AsConnection<IValidatable?, ValidatableValidators>();
}