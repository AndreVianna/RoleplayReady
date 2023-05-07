namespace System.Extensions;

public static class IntegerExtensions {
    public static IConnectors<int?, IntegerValidators> IsRequired(this int subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: false, subject, source!);
    public static IConnectors<int?, IntegerValidators> IsOptional(this int? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: true, subject, source!);
    public static IConnectors<int?, IntegerValidators> IsRequired(this int? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: false, subject, source!);
}