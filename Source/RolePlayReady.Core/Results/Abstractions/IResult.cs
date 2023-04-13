namespace System.Results.Abstractions;

public interface IResult<out TValue> : IValidation {
    bool HasValue { get; }
    TValue Value { get; }
}