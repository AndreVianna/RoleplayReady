namespace System.Validations.Extensions;

public static class NullOrDecimalExtension {
    public static INullOrDecimalValidations ValueIs(this decimal? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NullOrDecimalValidation(subject, source);
}