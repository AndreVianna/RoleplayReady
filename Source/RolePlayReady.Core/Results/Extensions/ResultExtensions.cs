using static System.Results.Result;

namespace System.Results.Extensions;

public static class ResultExtensions {
    public static Result<TOutput> ToResult<TOutput>(this Result input, TOutput value)
        => new Result<TOutput>(value) + input;

    public static Result<TOutput> Map<TInput, TOutput>(this Result<TInput> input, Func<TInput, TOutput> map)
        => new Result<TOutput>(map(input.Value)) + (input.HasErrors ? input.Errors.ToArray() : Success);

    public static Result<IEnumerable<TOutput>> Map<TInput, TOutput>(this Result<IEnumerable<TInput>> input, Func<TInput, TOutput> map)
        => new Result<IEnumerable<TOutput>>(input.Value.Select(map)) + (input.HasErrors ? input.Errors.ToArray() : Success);

    public static SearchResult<TOutput> ToSearchResult<TOutput>(this Result input, TOutput value)
        => new SearchResult<TOutput>(value) + input;

    public static SearchResult<TOutput> Map<TInput, TOutput>(this SearchResult<TInput> input, Func<TInput, TOutput> map)
        => new SearchResult<TOutput>(map(input.Value)) + (input.HasErrors ? input.Errors.ToArray() : Success);

    public static SearchResult<IEnumerable<TOutput>> Map<TInput, TOutput>(this SearchResult<IEnumerable<TInput>> input, Func<TInput, TOutput> map)
        => new SearchResult<IEnumerable<TOutput>>(input.Value.Select(map)) + (input.HasErrors ? input.Errors.ToArray() : Success);
}