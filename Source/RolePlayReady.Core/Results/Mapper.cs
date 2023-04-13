namespace System.Results;

public static class ResultExtensions {
    public static ResultOf<TOutput> Map<TInput, TOutput>(this ResultOf<TInput> input, Func<TInput, TOutput> map)
        => input.HasValue
            ? new(map(input.Value))
            : input.IsNull
                ? new()
                : new(input.Exception);

    public static ResultOf<IEnumerable<TOutput>> Map<TInput, TOutput>(this ResultOf<IEnumerable<TInput>> input, Func<TInput, TOutput> map)
        => input.HasValue
            ? new(input.Value.Select(map))
            : input.IsNull
                ? new()
                : new(input.Exception);
}