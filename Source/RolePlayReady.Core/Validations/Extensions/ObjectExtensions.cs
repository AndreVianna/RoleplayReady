namespace System.Validations.Extensions;

public static class ObjectExtensions {
    public static IConnectsToOrFinishes<IObjectValidation> IsNotNull(this object? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new ObjectValidation(subject, source, Validation.EnsureNotNull(subject, source));
}