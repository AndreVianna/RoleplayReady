namespace System.Validations.Extensions;

public static class IntegerExtensions {
    public static INumberValidations<int> IsNullOr(this int? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NumberValidations<int>(subject, source!);
    public static IConnects<INumberValidations<int>> IsNotNull(this int? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new Connects<INumberValidations<int>>(new NumberValidations<int>(subject, source!, Validation.EnsureNotNull(subject, source!)));
}