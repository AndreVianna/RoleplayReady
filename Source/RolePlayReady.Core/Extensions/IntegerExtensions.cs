namespace System.Extensions;

public static class IntegerExtensions {
    public static IConnectors<int?, IntegerValidators> IsRequired(this int subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => IntegerValidators.CreateAsRequired(subject, source!).AsConnection<int?, IntegerValidators>();
    public static IConnectors<int?, IntegerValidators> IsOptional(this int? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => IntegerValidators.CreateAsOptional(subject, source!).AsConnection<int?, IntegerValidators>();
    public static IConnectors<int?, IntegerValidators> IsRequired(this int? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => IntegerValidators.CreateAsRequired(subject, source!).AsConnection<int?, IntegerValidators>();
}