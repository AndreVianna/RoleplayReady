namespace System.Validators.Extensions;

public static class NumericExtension
{
    public static INumericChecks Is<TNumber>(this TNumber? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        where TNumber : INumber<TNumber>
        => new NumericValidator<TNumber>(subject, source);
}