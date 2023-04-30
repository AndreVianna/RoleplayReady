using static System.Results.Result;

namespace System.Results.Extensions;

public static class ResultExtensions {
    public static Result<TOutput> ToResult<TOutput>(this Result input, TOutput value)
        => WithErrors(value, input.Errors);

    public static Result<TOutput> Map<TInput, TOutput>(this Result<TInput> input, Func<TInput?, TOutput?> map)
        => WithErrors(map(input.Value), input.Errors);

    public static Result<IEnumerable<TOutput>> Map<TInput, TOutput>(this Result<IEnumerable<TInput>> input, Func<TInput, TOutput> map)
        => WithErrors(input.Value?.Select(map), input.Errors);

    public static ResultOrNotFound<TOutput> ToResultOrNotFound<TOutput>(this Result input, TOutput value, bool found)
        => ResultOrNotFound.WithErrors(value, found, input.Errors);

    public static ResultOrNotFound<TOutput> Map<TInput, TOutput>(this ResultOrNotFound<TInput> input, Func<TInput?, TOutput?> map, bool found)
        => ResultOrNotFound.WithErrors(map(input.Value), found, input.Errors);
}