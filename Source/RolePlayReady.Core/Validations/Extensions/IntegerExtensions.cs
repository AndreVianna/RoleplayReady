namespace System.Validations.Extensions;

public static class IntegerExtensions {
    public static IIntegerValidation Is(this int subject, [CallerArgumentExpression(nameof(subject))]string? source = null)
        => new IntegerValidation(subject, source);
    public static IIntegerValidation IsNullOr(this int? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new IntegerValidation(subject, source);
    public static IConnectsToOrFinishes<IIntegerValidation> IsNotNull(this int? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new IntegerValidation(subject, source, Validation.EnsureNotNull(subject, source));
}