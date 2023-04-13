namespace System.Results.Abstractions;

public interface IResult<out TObject> : IValidationResult {
    bool HasValue { get; }
    TObject Value { get; }
}