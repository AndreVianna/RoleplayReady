namespace System.Extensions;

public static class ValidatableExtensions {
    public static IConnectors<IValidatable?, ValidatableValidators> IsOptional(this IValidatable? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => ValidatableValidators.CreateAsOptional(subject, source!).AsConnection<IValidatable?, ValidatableValidators>();
    public static IConnectors<IValidatable?, ValidatableValidators> IsRequired(this IValidatable? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => ValidatableValidators.CreateAsRequired(subject, source!).AsConnection<IValidatable?, ValidatableValidators>();
}