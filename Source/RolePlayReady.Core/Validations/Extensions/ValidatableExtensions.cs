namespace System.Validations.Extensions;

public static class ValidatableExtensions {
    public static IConnects<IValidatableValidations> IsNotNull(this IValidatable? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new Connects<IValidatableValidations>(new ValidatableValidations(subject, source!, Validation.EnsureNotNull(subject, source!)));
}