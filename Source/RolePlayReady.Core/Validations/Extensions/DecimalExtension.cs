namespace System.Validations.Extensions;

public static class DecimalExtension {
    public static IDecimalValidations ValueIs(this decimal subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new DecimalValidation(subject, source);
}