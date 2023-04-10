namespace System.Results;

public readonly struct Maybe<TObject>
{
    private readonly OneOf<TObject, Default<TObject>, Exception> _result;

    public Maybe()
    {
        _result = Default<TObject>.Instance;
    }

    public Maybe(Result<TObject> result)
    {
        _result = result.HasValue ? result.Value : Exception;
    }

    public Maybe(TObject result)
    {
        _result = result;
    }

    public Maybe(Exception exception)
    {
        _result = exception;
    }

    public bool HasValue => _result.IsT0;
    public bool IsNull => _result.IsT1;

    public TObject Value => _result.IsT0
        ? _result.AsT0
        : _result.IsT2
            ? throw Exception
            : throw new InvalidOperationException("The value cannot be null.");
    public TObject? Default => _result.IsT1
        ? _result.AsT1.Value
        : _result.IsT2
            ? throw Exception
            : throw new InvalidOperationException("The value is not null.");
    public Exception Exception => _result.IsT2
        ? _result.AsT2
        : new InvalidOperationException("A value cannot be assigned to an exception.");

    public Maybe<TResult?> MapTo<TResult>(Func<TObject?, TResult?> map)
        => !_result.IsT2 ? new(map(Value)) : new(Exception);

    public void Throw()
    {
        if (_result.IsT2)
            throw Exception;
    }

    public static implicit operator Maybe<TObject>(Result<TObject> input) => new(input);
    public static implicit operator Maybe<TObject>(TObject? input) => input is null ? new() : new(input);
    public static implicit operator Maybe<TObject>(Exception input) => new(input);
    public static implicit operator Exception(Maybe<TObject> input) => input.Exception;
    public static implicit operator TObject?(Maybe<TObject> input) => input.IsNull ? input.Default : input.Value;
}