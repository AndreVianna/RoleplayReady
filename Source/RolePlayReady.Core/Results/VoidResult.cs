using System.Results.Abstractions;

namespace System.Results;

public class VoidResult : IResult {
    private readonly OneOf<Success, Exception> _result;

    public VoidResult() { }

    public VoidResult(Exception exception) {
        _result = exception;
    }

    public bool IsSuccess => _result.IsT0;
    public bool IsException => _result.IsT1;

    public Exception Exception => IsException
        ? _result.AsT1
        : throw new InvalidCastException(ResultHasNoExceptions);

    public void Throw() { if (_result.IsT1) throw _result.AsT1; }

    public static implicit operator VoidResult(Success _) => new();
    public static implicit operator VoidResult(Exception exception) => new(exception);
}