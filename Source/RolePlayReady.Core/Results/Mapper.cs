namespace System.Results;

public static class ResultExtensions {
    public static Result<TOutput> Map<TInput, TOutput>(this Result<TInput> input, Func<TInput, TOutput> map)
        => input.HasValue
            ? new(map(input.Value))
            : input.IsNull
                ? new()
                : new(input.Exception);

    public static Result<IEnumerable<TOutput>> Map<TInput, TOutput>(this Result<IEnumerable<TInput>> input, Func<TInput, TOutput> map)
        => input.HasValue
            ? new(input.Value.Select(map))
            : input.IsNull
                ? new()
                : new(input.Exception);
}