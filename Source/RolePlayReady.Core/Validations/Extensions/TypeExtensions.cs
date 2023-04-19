namespace System.Validations.Extensions;

public static class TypeExtensions {
    public static IConnectsToOrFinishes<ITypeValidators> IsNotNull(this Type? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new TypeValidation(subject, source, Validation.EnsureNotNull(subject, source));
}