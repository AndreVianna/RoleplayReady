using System.Validation;
using System.Validation.Builder;

namespace System.Extensions;

public static class ObjectExtensions {
    public static IConnectors<object?, ObjectValidators> IsNotNull(this object? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => ObjectValidators.CreateAsRequired(subject, source!).AsConnection<object?, ObjectValidators>();
    public static IConnectors<IValidatable?, ValidatableValidators> IsOptional(this IValidatable? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => ValidatableValidators.CreateAsOptional(subject, source!).AsConnection<IValidatable?, ValidatableValidators>();
    public static IConnectors<IValidatable?, ValidatableValidators> IsRequired(this IValidatable? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => ValidatableValidators.CreateAsRequired(subject, source!).AsConnection<IValidatable?, ValidatableValidators>();
}