namespace System.Results.Abstractions;

public interface IResult {
    bool IsSuccess { get; }
    IList<ValidationError> Errors { get; }
}

public interface IResult<out TValue> : IResult {
    TValue? Value { get; }
}