namespace System.Results;

public static class ResultExtensions {
    public static Result<TOutput> Map<TInput, TOutput>(this Result<TInput> input, Func<TInput, TOutput> map)
        => new Result<TOutput>(map(input.Value)) + (input.HasErrors ? input.Errors.ToArray() : Success.Instance);

    public static Result<IEnumerable<TOutput>> Map<TInput, TOutput>(this Result<IEnumerable<TInput>> input, Func<TInput, TOutput> map)
        => new Result<IEnumerable<TOutput>>(input.Value.Select(map)) + (input.HasErrors ? input.Errors.ToArray() : Success.Instance);

    public static Maybe<TOutput> Map<TInput, TOutput>(this Maybe<TInput> input, Func<TInput?, TOutput?> map)
        => new Maybe<TOutput>(map(input.Value)) + (input.HasErrors ? input.Errors.ToArray() : Success.Instance);

    public static Maybe<IEnumerable<TOutput>> Map<TInput, TOutput>(this Maybe<IEnumerable<TInput>> input, Func<TInput, TOutput> map)
        => new Maybe<IEnumerable<TOutput>>(input.Value?.Select(map)) + (input.HasErrors ? input.Errors.ToArray() : Success.Instance);
}