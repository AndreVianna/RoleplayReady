namespace System.Extensions;

public static class DateTimeExtensions {
    public static IConnectors<DateTime?, DateTimeValidators> IsRequired(this DateTime subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: false, subject, source!);
    public static IConnectors<DateTime?, DateTimeValidators> IsOptional(this DateTime? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: true, subject, source!);
    public static IConnectors<DateTime?, DateTimeValidators> IsRequired(this DateTime? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(allowNull: false, subject, source!);
}