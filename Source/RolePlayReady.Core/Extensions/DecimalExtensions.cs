namespace System.Extensions;

public static class DecimalExtensions {
    public static IConnectors<decimal?, DecimalValidators> IsRequired(this decimal subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: false, subject, source!);
    public static IConnectors<decimal?, DecimalValidators> IsOptional(this decimal? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: true, subject, source!);
    public static IConnectors<decimal?, DecimalValidators> IsRequired(this decimal? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: false, subject, source!);
}