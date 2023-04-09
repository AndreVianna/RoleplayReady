namespace RolePlayReady.Results;

public class MethodResult {
    private readonly OneOf<Success, Exception> _result;

    public MethodResult() {
        _result = new Success();
    }

    private MethodResult(Exception exception) {
        _result = exception;
    }

    public bool IsSuccess => _result.IsT0;
    public Exception Exception => _result.AsT1;

    public static implicit operator MethodResult(Success _) => new();
    public static implicit operator MethodResult(Exception exception) => new(exception);
}

public class MethodResult<TResult> {
    private readonly OneOf<TResult, Exception> _result;

    public MethodResult(TResult result) {
        _result = result;
    }

    private MethodResult(Exception exception) {
        _result = exception;
    }

    public bool HasValue => _result.IsT0;
    public TResult Value => _result.AsT0;
    public Exception Exception => _result.AsT1;
    public void Throw() => throw Exception;

    public static implicit operator MethodResult<TResult>(TResult result) => new(result);
    public static implicit operator MethodResult<TResult>(Exception exception) => new(exception);
}