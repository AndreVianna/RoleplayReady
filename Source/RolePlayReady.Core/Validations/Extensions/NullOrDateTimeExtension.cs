namespace System.Validations.Extensions;

public static class NullOrDateTimeExtension {
    public static INullOrDateTimeValidations ValueIs(this DateTime? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NullOrDateTimeValidation(subject, source);
}