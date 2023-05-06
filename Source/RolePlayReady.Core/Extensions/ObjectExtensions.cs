namespace System.Extensions;

public static class ObjectExtensions {
    public static IConnectors<object?, ObjectValidators> IsNotNull(this object? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => ObjectValidators.CreateAsRequired(subject, source!).AsConnection<object?, ObjectValidators>();
}
