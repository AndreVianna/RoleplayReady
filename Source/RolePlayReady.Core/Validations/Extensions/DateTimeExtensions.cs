namespace System.Validations.Extensions;

public static class DateTimeExtensions {
    public static IDateTimeValidation Is(this DateTime subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new DateTimeValidation(subject, source);
    public static IDateTimeValidation IsNullOr(this DateTime? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new DateTimeValidation(subject, source);
    public static IConnectsToOrFinishes<IDateTimeValidation> IsNotNull(this DateTime? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new DateTimeValidation(subject, source, Validation.EnsureNotNull(subject, source));
}