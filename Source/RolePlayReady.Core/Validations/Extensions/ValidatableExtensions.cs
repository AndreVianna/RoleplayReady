namespace System.Validations.Extensions;

public static class ValidatableExtensions {
    public static IValidationsConnector<IValidatable?, ValidatableValidations> IsOptional(this IValidatable? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => ValidatableValidations.CreateAsOptional(subject, source!).AsConnection<IValidatable?, ValidatableValidations>();
    public static IValidationsConnector<IValidatable?, ValidatableValidations> IsRequired(this IValidatable? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => ValidatableValidations.CreateAsRequired(subject, source!).AsConnection<IValidatable?, ValidatableValidations>();
}