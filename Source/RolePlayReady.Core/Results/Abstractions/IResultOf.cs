namespace System.Results.Abstractions;

public interface IResultOf<out TObject> : IValidationResult {
    bool HasValue { get; }
    TObject Value { get; }
}