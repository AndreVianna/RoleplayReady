namespace System.Validations.Extensions;

public static class DateTimeExtensions {
    public static IDateTimeValidators Value(this DateTime subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new DateTimeValidation(subject, source);
    public static IDateTimeValidators IsNullOr(this DateTime? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new DateTimeValidation(subject, source);
    public static IConnectsToOrFinishes<IDateTimeValidators> IsNotNull(this DateTime? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new DateTimeValidation(subject, source, Validation.EnsureNotNull(subject, source));
}