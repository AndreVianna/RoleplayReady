using OneOf.Types;

namespace System.Results;

public abstract class Result : IResult {
    private readonly OneOf<Success, Exception> _result;

    protected Result() { }

    protected Result(Exception exception) {
        _result = exception;
    }

    public bool IsSuccess => _result.IsT0;
    public bool IsException => _result.IsT1;

    public Exception Exception => IsException
        ? _result.AsT1
        : throw new InvalidCastException(ResultHasNoExceptions);

    public void Throw() {
        if (IsException)
            throw Exception;
    }
}

public abstract class Result<TResult, TObject> : IResult<TObject>
    where TResult : Result<TResult, TObject>, new() {

    // ReSharper disable once StaticMemberInGenericType - Only accessed locally
    private OneOf<TObject?, Failure, Exception> _result;

    protected Result() { }

    public IResult Valid => new TResult();

    protected Result(TObject? value) {
        _result = value;
    }

    protected Result(ICollection<ValidationError> errors) {
        _result = ResultFactory.Invalid(errors);
    }

    protected Result(Exception exception) {
        _result = exception;
    }

    public bool IsSuccess => _result.IsT0;
    public bool HasValue => IsSuccess && ObjectValue is not null;
    public abstract bool IsNull { get; }
    public bool HasErrors => _result.IsT1;
    public bool IsException => _result.IsT2;

    [NotNull]
    public TObject Value => HasValue
        ? ObjectValue!
        : throw (IsException ? Exception : new InvalidCastException(ResultHasNoValue));

    public abstract TObject? Default { get; }

    protected TObject? ObjectValue => IsSuccess
        ? _result.AsT0
        : throw (IsException ? Exception : new InvalidCastException(ResultIsNotValid));

    public Exception Exception => IsException
        ? _result.AsT2
        : throw new InvalidCastException(ResultHasNoExceptions);

    public ICollection<ValidationError> Errors
        => IsSuccess
            ? NoErrors
            : HasErrors
                ? _result.AsT1.Errors
                : throw Exception;


    public virtual bool TryGetValue(out TObject? value) {
        value = default;
        try {
            value = _result.AsT0;
        }
        catch {
            return false;
        }

        return true;
    }

    public bool TryGetErrors(out IEnumerable<ValidationError>? errors) {
        errors = null;
        try {
            errors = _result.AsT1.Errors;
        }
        catch {
            return false;
        }

        return true;
    }

    [DoesNotReturn]
    public void Throw() => throw _result.AsT2;

    protected TResult AddErrors(ICollection<ValidationError> errors) {
        var validationErrors = Ensure.NotNullOrHasNull(errors);
        return validationErrors.Any()
            ? HasErrors
                ? CreateFor(Errors.Concat(validationErrors).ToArray())
                : CreateFor(validationErrors)
            : (TResult)this;
    }

    protected static TResult CreateFor(object? input) {
        var result = new TResult();
        result.SetResultTypeFrom(input);
        return result;
    }

    private void SetResultTypeFrom(object? input)
        => _result = input switch {
            Result<TResult, TObject> result => result._result,
            TObject value => value,
            ICollection<ValidationError> errors => ResultFactory.Invalid(errors),
            ValidationError error => ResultFactory.Invalid(error),
            Exception exception => exception,
            _ => _result
        };
}
