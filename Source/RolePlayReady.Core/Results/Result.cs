namespace System.Results;

public readonly struct Result<TObject>
{
    private readonly OneOf<TObject, Exception> _result;

    public Result(Result<TObject> result)
    {
        _result = result._result;
    }

    public Result(TObject result)
    {
        _result = result;
    }

    public Result(Exception exception)
    {
        _result = exception;
    }

    public bool HasValue => _result.IsT0;

    public TObject Value => !_result.IsT0 ? throw Exception : _result.AsT0;
    public Exception Exception => _result.IsT1 ? _result.AsT1 : throw new InvalidOperationException($"Cannot return as 'Exception' a successful input of '{typeof(TObject).Name}'.");

    public void Throw()
    {
        if (_result.IsT1)
            throw Exception;
    }

    public static implicit operator Result<TObject>(Maybe<TObject> input) => input.HasValue
        ? new(input.Value)
        : !input.IsNull
            ? new(input.Exception)
            : new((Exception)new InvalidOperationException("The value cannot be null null."));
    public static implicit operator Result<TObject>(TObject? input) => input is not null
        ? new(input)
        : new((Exception)new InvalidOperationException("The value cannot be null null."));
    public static implicit operator Result<TObject>(Exception input) => new(input);
    public static implicit operator Exception(Result<TObject> input) => input.Exception;
    public static implicit operator TObject(Result<TObject> input) => input.Value;
}