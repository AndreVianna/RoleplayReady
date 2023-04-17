namespace System.Results.Abstractions;

public interface INullableResult<out TValue> : IResult {
    bool IsNull { get; }
    bool HasValue { get; }
    TValue? Value { get; }
}