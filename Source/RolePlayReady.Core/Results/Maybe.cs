namespace System.Results;

public readonly struct Maybe<TObject> {
    private readonly OneOf<TObject, Default<TObject>, Exception> _result;

    public Maybe() {
        _result = Default<TObject>.Instance;
    }

    public Maybe(TObject result) {
        _result = result;
    }

    public Maybe(Exception exception) {
        _result = exception;
    }

    public bool HasValue => _result.IsT0;
    public bool IsNull => _result.IsT1;

    public TObject Value => _result.IsT0
        ? _result.AsT0
        : _result.IsT2
            ? throw Exception
            : throw new InvalidCastException($"Cannot return as 'Default<{typeof(TObject).Name}>' a '{typeof(TObject).Name}' value.");
    public TObject? Default => _result.IsT1
        ? _result.AsT1.Value
        : _result.IsT2
            ? throw Exception
            : throw new InvalidCastException($"Cannot return as '{typeof(TObject).Name}' a 'Default<{typeof(TObject).Name}>' value.");
    public Exception Exception => _result.IsT2
        ? _result.AsT2
        : throw new InvalidCastException($"Cannot return as 'Exception' a '{typeof(TObject).Name}?' value.");

    public void Throw() {
        if (_result.IsT2)
            throw Exception;
    }

    public static implicit operator Maybe<TObject>(Result<TObject> input) => input.HasValue
        ? new(input.Value)
        : new(input.Exception);
    public static implicit operator Maybe<TObject>(TObject? input) => input is null
        ? new()
        : new(input);
    public static implicit operator Maybe<TObject>(Exception input) => new(input);
    public static implicit operator Exception(Maybe<TObject> input) => input.Exception;
    public static implicit operator TObject?(Maybe<TObject> input) => input.IsNull
        ? input.Default
        : input.Value;
}