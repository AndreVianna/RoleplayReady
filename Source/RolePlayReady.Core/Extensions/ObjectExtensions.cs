namespace System.Extensions;

public static class ObjectExtensions {
    public static IConnectors<object?, ObjectValidators> IsRequired(this object? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: false, subject, source!);
    public static IConnectors<object?, ObjectValidators> IsOptional(this object? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: true, subject, source!);
}
