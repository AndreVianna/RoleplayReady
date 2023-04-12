namespace System.Results;

public static class ResultExtensions {
    public static Maybe<TOutput> Map<TInput, TOutput>(this Maybe<TInput> input, Func<TInput, TOutput> map)
        => input.HasValue
            ? new(map(input.Value))
            : input.IsNull
                ? new()
                : new(input.Exception);

    public static Maybe<IEnumerable<TOutput>> Map<TInput, TOutput>(this Maybe<IEnumerable<TInput>> input, Func<TInput, TOutput> map)
        => input.HasValue
            ? new(input.Value.Select(map))
            : input.IsNull
                ? new()
                : new(input.Exception);

    public static Object<TOutput> Map<TInput, TOutput>(this Object<TInput> input, Func<TInput, TOutput> map)
        => input.HasValue
            ? new(map(input.Value))
            : new(input.Exception);

    public static Object<IEnumerable<TOutput>> Map<TInput, TOutput>(this Object<IEnumerable<TInput>> input, Func<TInput, TOutput> map)
        => input.HasValue
            ? new(input.Value.Select(map))
            : new(input.Exception);
}