using static System.Results.ValidationResult;

namespace System.Results.Extensions;

public static class ResultExtensions
{
    public static Result<TOutput> Map<TInput, TOutput>(this Result<TInput> input, Func<TInput, TOutput> map)
        => new Result<TOutput>(map(input.Value)) + (input.HasErrors ? input.Errors.ToArray() : Success);

    public static Result<IEnumerable<TOutput>> Map<TInput, TOutput>(this Result<IEnumerable<TInput>> input, Func<TInput, TOutput> map)
        => new Result<IEnumerable<TOutput>>(input.Value.Select(map)) + (input.HasErrors ? input.Errors.ToArray() : Success);

    public static NullableResult<TOutput> Map<TInput, TOutput>(this NullableResult<TInput> input, Func<TInput?, TOutput?> map)
        => new NullableResult<TOutput>(map(input.Value)) + (input.HasErrors ? input.Errors.ToArray() : Success);

    public static NullableResult<IEnumerable<TOutput>> Map<TInput, TOutput>(this NullableResult<IEnumerable<TInput>> input, Func<TInput, TOutput> map)
        => new NullableResult<IEnumerable<TOutput>>(input.Value?.Select(map)) + (input.HasErrors ? input.Errors.ToArray() : Success);
}