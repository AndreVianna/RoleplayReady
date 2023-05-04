namespace System.Validations.Extensions;

public static class TypeExtensions {
    public static IConnects<ITypeValidations> IsNotNull(this Type? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new Connects<ITypeValidations>(new TypeValidations(subject, source!, Validation.EnsureNotNull(subject, source!)));
}