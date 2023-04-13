namespace System.Results.Abstractions;

public interface IResult<out TObject> : IValidation {
    bool HasValue { get; }
    TObject Value { get; }
}