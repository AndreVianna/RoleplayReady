namespace System.Results.Extensions;

public static class ResultExtensions
{
    public static ObjectResult<TOutput> Map<TInput, TOutput>(this ObjectResult<TInput> input, Func<TInput, TOutput> map)
        => new ObjectResult<TOutput>(map(input.Value)) + (input.HasErrors ? input.Errors.ToArray() : SuccessfulResult.Success);

    public static ObjectResult<IEnumerable<TOutput>> Map<TInput, TOutput>(this ObjectResult<IEnumerable<TInput>> input, Func<TInput, TOutput> map)
        => new ObjectResult<IEnumerable<TOutput>>(input.Value.Select(map)) + (input.HasErrors ? input.Errors.ToArray() : SuccessfulResult.Success);

    public static NullableResult<TOutput> Map<TInput, TOutput>(this NullableResult<TInput> input, Func<TInput?, TOutput?> map)
        => new NullableResult<TOutput>(map(input.Value)) + (input.HasErrors ? input.Errors.ToArray() : SuccessfulResult.Success);

    public static NullableResult<IEnumerable<TOutput>> Map<TInput, TOutput>(this NullableResult<IEnumerable<TInput>> input, Func<TInput, TOutput> map)
        => new NullableResult<IEnumerable<TOutput>>(input.Value?.Select(map)) + (input.HasErrors ? input.Errors.ToArray() : SuccessfulResult.Success);
}