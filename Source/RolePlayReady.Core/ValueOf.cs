namespace System;

public readonly struct ValueOf<TResult> {
    private readonly OneOf<TResult, Exception> _result;

    public ValueOf(TResult result) {
        _result = result;
    }

    public ValueOf(Exception exception) {
        _result = exception;
    }

    public bool IsSuccessful => _result.IsT0;

    public TResult Value => !_result.IsT0 ? throw Exception : _result.AsT0;
    public Exception Exception => _result.IsT1 ? _result.AsT1 : throw new InvalidOperationException($"Cannot return as 'Exception' a successful result of '{typeof(TResult).Name}'.");

    public void Throw() {
        if (_result.IsT1)
            throw Exception;
    }

    public static implicit operator ValueOf<TResult>(TResult result) => new(result);
    public static implicit operator ValueOf<TResult>(Exception exception) => new(exception);
}