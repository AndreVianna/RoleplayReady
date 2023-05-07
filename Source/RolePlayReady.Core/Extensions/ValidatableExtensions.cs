namespace System.Extensions;

public static class ValidatableExtensions {
    public static IConnectors<IValidatable?, ValidatableValidators> IsOptional(this IValidatable? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: true, subject, source!);
    public static IConnectors<IValidatable?, ValidatableValidators> IsRequired(this IValidatable? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: false, subject, source!);
}