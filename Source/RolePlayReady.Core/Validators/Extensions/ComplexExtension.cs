namespace System.Validators.Extensions;

public static class ComplexExtension {
    public static IComplexChecks ValueIs<TComplex>(this TComplex? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        where TComplex : class, IValidatable
        => new ComplexValidator<TComplex>(subject, source);
}