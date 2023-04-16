namespace System.Validations.Extensions;

public static class NullableDecimalExtension {
    public static INullableDecimalValidation Is(this decimal? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NullableDecimalValidation(subject, source);
}