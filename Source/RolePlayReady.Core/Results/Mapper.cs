namespace System.Results;

public static class Mapper
{
    public static Maybe<TResult> Map<TInput, TResult>(this Maybe<TInput> input, Func<TInput, TResult> map)
        => input.HasValue
            ? new(map(input.Value))
            : input.IsNull
                ? new()
                : new(input.Exception);

    public static Maybe<IEnumerable<TResult>> Map<TInput, TResult>(this Maybe<IEnumerable<TInput>> input, Func<TInput, TResult> map)
        => input.HasValue
            ? new(input.Value.Select(map))
            : input.IsNull
                ? new()
                : new(input.Exception);

    public static Result<TResult> Map<TInput, TResult>(this Result<TInput> input, Func<TInput, TResult> map)
        => input.HasValue
            ? new(map(input.Value))
            : new(input.Exception);

    public static Result<IEnumerable<TResult>> Map<TInput, TResult>(this Result<IEnumerable<TInput>> input, Func<TInput, TResult> map)
        => input.HasValue
            ? new(input.Value.Select(map))
            : new(input.Exception);
}