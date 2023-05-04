namespace System.Validations.Extensions;

public static class TypeExtensions {
    public static IValidationsConnector<Type?, TypeValidations> IsRequired(this Type? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => TypeValidations.CreateAsRequired(subject, source!).AsConnection<Type?, TypeValidations>();
}