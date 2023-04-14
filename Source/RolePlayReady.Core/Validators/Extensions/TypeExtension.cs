namespace System.Validators.Extensions;

public static class TypeExtension {
    public static ITypeChecks Is(this Type? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new TypeValidator(subject, source);
}