namespace System.Validations.Extensions;

public static class NullableIntegerExtension {
    public static INullableIntegerValidation Is(this int? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NullableIntegerValidation(subject, source);
}