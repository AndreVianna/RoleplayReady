namespace System.Validations.Extensions;

public static class DecimalExtension {
    public static IDecimalValidation Is(this decimal subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new DecimalValidation(subject, source);
}