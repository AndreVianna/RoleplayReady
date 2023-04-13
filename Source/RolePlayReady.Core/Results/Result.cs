using System.Results.Abstractions;

namespace System.Results;

public class Result<TObject> : IResult<TObject> {
    private OneOf<TObject?, Failure, Exception> _result;

    public Result() { _result = default; }

    public Result(object? input) {
        _result = input switch {
            null => default,
            Result<TObject> result => result._result,
            ICollection<ValidationError> errors => new Failure(errors),
            ValidationError error => new Failure(error),
            Exception exception => exception,
            TObject value => value,
            _ => throw new InvalidCastException(string.Format(ResultInvalidType)),
        };
    }

    public bool IsSuccess => _result.IsT0;
    public bool HasValue => _result.IsT0 && _result.AsT0 is not null;
    public bool IsNull => _result.IsT0 && _result.AsT0 is null;
    public bool HasErrors => _result.IsT1;
    public bool IsException => _result.IsT2;

    [NotNull]
    public TObject Value => HasValue
        ? _result.AsT0!
        : throw (IsException ? Exception : new InvalidCastException(ResultHasNoValue));

    public TObject? Default => IsNull
        ? default
        : throw (IsException ? Exception : new InvalidCastException(ResultIsNotNull));

    public ICollection<ValidationError> Errors
        => IsSuccess
            ? NoErrors
            : HasErrors
                ? _result.AsT1.Errors
                : throw Exception;

    public Exception Exception => IsException
        ? _result.AsT2
        : throw new InvalidCastException(ResultHasNoExceptions);

    public void Throw() { if (_result.IsT2) throw _result.AsT2; }

    protected Result<TObject> AddErrors(ICollection<ValidationError> errors) {
        var validationErrors = Ensure.NotNullOrHasNull(errors);
        if (!validationErrors.Any() || IsException)
            return this;

        if (!HasErrors) {
            this._result = new Failure(validationErrors);
            return this;
        }

        foreach (var error in validationErrors)
            this._result.AsT1.Errors.Add(error);
        return this;
    }

    public static implicit operator Result<TObject>(TObject? value) => new(value);
    public static implicit operator Result<TObject>(Exception exception) => new(exception);
    public static implicit operator Result<TObject>(Failure failure) => new(failure.Errors);
    public static implicit operator Result<TObject>(List<ValidationError> errors) => new(errors);
    public static implicit operator Result<TObject>(ValidationError[] errors) => new(errors);
    public static implicit operator Result<TObject>(ValidationError error) => new(error);

    public static implicit operator TObject?(Result<TObject> input) => input.HasValue ? input.Value : input.Default;

    public static Result<TObject> operator +(Result<TObject> left, ValidationResult right) {
        if (right.IsException)
            left._result = right.Exception;
        else if (right.HasErrors)
            left.AddErrors(right.Errors);
        return left;
    }

    public static Result<TObject> operator +(Result<TObject> left, Success _) => left;
    public static Result<TObject> operator +(Result<TObject> left, ValidationError right) => left.AddErrors(new[] { right });
    public static Result<TObject> operator +(Result<TObject> left, ICollection<ValidationError> right) => left.AddErrors(right);
}