using System.Results.Abstractions;

namespace System.Results;

public class Validation : IValidation {
    private OneOf<Success, Failure, Exception> _result;

    public Validation() { _result = Success.Instance; }

    public Validation(object? input) {
        _result = input switch {
            Validation result => result._result,
            ICollection<ValidationError> errors => new Failure(errors),
            ValidationError error => new Failure(error),
            Exception exception => exception,
            Success value => value,
            _ => throw new InvalidCastException(string.Format(ResultInvalidType)),
        };
    }

    public bool IsSuccess => _result.IsT0;
    public bool HasErrors => _result.IsT1;
    public bool IsException => _result.IsT2;

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

    protected Validation AddErrors(ICollection<ValidationError> errors) {
        var validationErrors = Ensure.NotNullOrHasNull(errors);
        if (!validationErrors.Any() || IsException)
            return this;

        if (!HasErrors) {
            _result = new Failure(validationErrors);
            return this;
        }

        foreach (var error in validationErrors)
            _result.AsT1.Errors.Add(error);
        return this;
    }

    public static implicit operator Validation(Exception exception) => new(exception);
    public static implicit operator Validation(Failure failure) => new(failure.Errors);
    public static implicit operator Validation(List<ValidationError> errors) => new(errors);
    public static implicit operator Validation(ValidationError[] errors) => new(errors);
    public static implicit operator Validation(ValidationError error) => new(error);

    public static Validation operator +(Validation left, Validation right) {
        if (right.IsException)
            left._result = right.Exception;
        else if (right.HasErrors)
            left.AddErrors(right.Errors);
        return left;
    }

    public static Validation operator +(Validation left, Success _) => left;
    public static Validation operator +(Validation left, ValidationError right) => left.AddErrors(new[] { right });
    public static Validation operator +(Validation left, ICollection<ValidationError> right) => left.AddErrors(right);
}