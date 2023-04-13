namespace System.Results.Abstractions;

public interface IMaybe<out TValue> : IValidation {
    bool IsNull { get; }
    bool HasValue { get; }
    TValue? Value { get; }
}