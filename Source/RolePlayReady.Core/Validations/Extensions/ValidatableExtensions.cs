namespace System.Validations.Extensions;

public static class ValidatableExtensions {
    public static IConnectsToOrFinishes<IValidatableValidation> IsNotNull(this IValidatable? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new ValidatableValidation(subject, source, Validation.EnsureNotNull(subject, source));
}