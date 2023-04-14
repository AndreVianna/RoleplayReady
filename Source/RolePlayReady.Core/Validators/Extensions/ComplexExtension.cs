namespace System.Validators.Extensions;

public static class ComplexExtension {
    public static IComplexChecks Is<TComplex>(this TComplex? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        where TComplex : IValidatable
        => new ComplexValidator<TComplex>(subject, source);
}