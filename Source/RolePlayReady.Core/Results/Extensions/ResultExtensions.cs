using static System.Results.ResultOrNotFound;

namespace System.Results.Extensions;

public static class ResultExtensions {
    public static Result<TOutput> ToResult<TOutput>(this Result input, TOutput value)
        => Result.AsSuccessFor(value) + input.Errors.ToArray();

    public static Result<TOutput> Map<TInput, TOutput>(this Result<TInput> input, Func<TInput, TOutput> map)
        => Result.AsSuccessFor(map(input.Value!)) + input.Errors.ToArray();

    public static Result<IEnumerable<TOutput>> Map<TInput, TOutput>(this Result<IEnumerable<TInput>> input, Func<TInput, TOutput> map)
        => Result.AsSuccessFor(input.Value!.Select(map)) + input.Errors.ToArray();

    public static ResultOrNotFound<TOutput> ToResultOrNotFound<TOutput>(this Result input, bool found, TOutput value)
        => (found ? AsSuccessFor(value) : AsNotFoundFor(value)) + input.Errors.ToArray();

    public static ResultOrNotFound<TOutput> Map<TInput, TOutput>(this ResultOrNotFound<TInput> input, bool found, Func<TInput, TOutput> map)
        => (found ? AsSuccessFor(map(input.Value!)) : AsNotFoundFor(map(input.Value!))) + input.Errors.ToArray();
}