namespace System.Results.Abstractions;

public interface INullableResult<out TValue> : IResult {
    TValue? Value { get; }
}