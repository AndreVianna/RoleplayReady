using System.Results.Abstractions;

namespace System.Results;

public class ResultOf<TObject> : IResultOf<TObject> {
    private OneOf<TObject?, Failure, Exception> _result;

    public ResultOf() { _result = default; }

    public ResultOf(object? input) {
        _result = input switch {
            null => default,
            ResultOf<TObject> result => result._result,
            ICollection<ValidationError> errors => new Failure(errors),
            ValidationError error => new Failure(error),
            Exception exception => exception,
            TObject value => value,
            _ => throw new InvalidCastException(string.Format(ResultInvalidType)),
        };
    }

    public bool IsSuccess => _result.IsT0;
    public bool HasValue => _result.IsT0 && _result.AsT0 is not null;
    public bool HasErrors => _result.IsT1;
    public bool IsException => _result.IsT2;

    [NotNull]
    public TObject Value => HasValue
        ? _result.AsT0!
        : throw (IsException ? Exception : new InvalidCastException(ResultHasNoValue));

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

    protected ResultOf<TObject> AddErrors(ICollection<ValidationError> errors) {
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

    public static implicit operator ResultOf<TObject>(TObject? value) => new(value);
    public static implicit operator ResultOf<TObject>(Exception exception) => new(exception);
    public static implicit operator ResultOf<TObject>(Failure failure) => new(failure.Errors);
    public static implicit operator ResultOf<TObject>(List<ValidationError> errors) => new(errors);
    public static implicit operator ResultOf<TObject>(ValidationError[] errors) => new(errors);
    public static implicit operator ResultOf<TObject>(ValidationError error) => new(error);

    public static implicit operator TObject(ResultOf<TObject> input) => input.Value;

    public static ResultOf<TObject> operator +(ResultOf<TObject> left, ValidationResult right) {
        if (right.IsException)
            left._result = right.Exception;
        else if (right.HasErrors)
            left.AddErrors(right.Errors);
        return left;
    }

    public static ResultOf<TObject> operator +(ResultOf<TObject> left, Success _) => left;
    public static ResultOf<TObject> operator +(ResultOf<TObject> left, ValidationError right) => left.AddErrors(new[] { right });
    public static ResultOf<TObject> operator +(ResultOf<TObject> left, ICollection<ValidationError> right) => left.AddErrors(right);
}