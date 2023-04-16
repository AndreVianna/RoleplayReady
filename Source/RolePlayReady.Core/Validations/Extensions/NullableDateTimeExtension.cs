namespace System.Validations.Extensions;

public static class NullableDateTimeExtension {
    public static INullableDateTimeValidation Is(this DateTime? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NullableDateTimeValidation(subject, source);
}