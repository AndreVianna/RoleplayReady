namespace System.Validations.Extensions;

public static class IntegerExtensions {
    public static INumberValidators<int> IsNullOr(this int? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NumberValidation<int>(subject, source);
    public static IConnectsToOrFinishes<INumberValidators<int>> IsNotNull(this int? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NumberValidation<int>(subject, source, Validation.EnsureNotNull(subject, source));
}