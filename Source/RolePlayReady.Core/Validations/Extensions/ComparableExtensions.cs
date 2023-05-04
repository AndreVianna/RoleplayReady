namespace System.Validations.Extensions;

public static class ComparableExtensions {
    //public static IValidationsConnector<int, NumberValidations<int>> Value(this int subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
    //    => NumberValidations<int>.CreateAsRequired(subject, source!).AsConnection<int, NumberValidations<int>>();
    public static IValidationsConnector<TValue, NumberValidations<TValue>> IsOptional<TValue>(this TValue? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        where TValue : IComparable<TValue>
        => NumberValidations<TValue>.CreateAsOptional(subject, source!).AsConnection<TValue, NumberValidations<TValue>>();
    public static IValidationsConnector<TValue, NumberValidations<TValue>> IsRequired<TValue>(this TValue? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        where TValue : IComparable<TValue>
        => NumberValidations<TValue>.CreateAsRequired(subject, source!).AsConnection<TValue, NumberValidations<TValue>>();
}