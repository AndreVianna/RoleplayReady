using System.Results.Abstractions;

namespace System.Results;

public class ValidationResult : IValidationResult {
    private OneOf<Success, Failure, Exception> _result;

    public ValidationResult() { _result = Success.Instance; }

    public ValidationResult(object? input) {
        _result = input switch {
            ValidationResult result => result._result,
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

    protected ValidationResult AddErrors(ICollection<ValidationError> errors) {
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

    public static implicit operator ValidationResult(Exception exception) => new(exception);
    public static implicit operator ValidationResult(Failure failure) => new(failure.Errors);
    public static implicit operator ValidationResult(List<ValidationError> errors) => new(errors);
    public static implicit operator ValidationResult(ValidationError[] errors) => new(errors);
    public static implicit operator ValidationResult(ValidationError error) => new(error);

    public static ValidationResult operator +(ValidationResult left, ValidationResult right) {
        if (right.IsException)
            left._result = right.Exception;
        else if (right.HasErrors)
            left.AddErrors(right.Errors);
        return left;
    }

    public static ValidationResult operator +(ValidationResult left, Success _) => left;
    public static ValidationResult operator +(ValidationResult left, ValidationError right) => left.AddErrors(new[] { right });
    public static ValidationResult operator +(ValidationResult left, ICollection<ValidationError> right) => left.AddErrors(right);
}