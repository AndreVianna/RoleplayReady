namespace System.Results;

public static class ResultExtensions {
    public static Result<TOutput> Map<TInput, TOutput>(this Result<TInput> input, Func<TInput, TOutput> map)
        => input.HasValue
            ? new(map(input.Value))
            : input.HasErrors
                ? new(input.Errors)
                : throw new InvalidCastException(ResultIsNull);

    public static Result<IEnumerable<TOutput>> Map<TInput, TOutput>(this Result<IEnumerable<TInput>> input, Func<TInput, TOutput> map)
        => input.HasValue
            ? new(input.Value.Select(map))
            : input.HasErrors
                ? new(input.Errors)
                : throw new InvalidCastException(ResultIsNull);

    public static Maybe<TOutput> Map<TInput, TOutput>(this Maybe<TInput> input, Func<TInput, TOutput> map)
        => input.HasValue
            ? new(map(input.Value))
            : input.IsNull
                ? new()
                : new(input.Errors);

    public static Maybe<IEnumerable<TOutput>> Map<TInput, TOutput>(this Maybe<IEnumerable<TInput>> input, Func<TInput, TOutput> map)
        => input.HasValue
            ? new(input.Value.Select(map))
            : input.IsNull
                ? new()
                : new(input.Errors);
}