using System.Results.Abstractions;

namespace System.Results;

public class Validation : IValidation {
    private OneOf<Success, Failure> _result;

    public Validation() { _result = Success.Instance; }

    public Validation(object? input) {
        _result = input switch {
            Validation result => result._result,
            ICollection<ValidationError> errors => new Failure(errors),
            ValidationError error => new Failure(error),
            Success value => value,
            _ => throw new InvalidCastException(string.Format(ResultInvalidType)),
        };
    }

    public bool IsSuccess => _result.IsT0;
    public bool HasErrors => _result.IsT1;

    public ICollection<ValidationError> Errors
        => _result.IsT0
            ? NoErrors
            : _result.AsT1.Errors;

    protected Validation AddErrors(ICollection<ValidationError> errors) {
        var validationErrors = Ensure.NotNullOrHasNull(errors);
        if (!validationErrors.Any())
            return this;

        if (!HasErrors) {
            _result = new Failure(validationErrors);
            return this;
        }

        foreach (var error in validationErrors)
            Errors.Add(error);
        return this;
    }

    public static implicit operator Validation(Failure failure) => new(failure.Errors);
    public static implicit operator Validation(List<ValidationError> errors) => new(errors);
    public static implicit operator Validation(ValidationError[] errors) => new(errors);
    public static implicit operator Validation(ValidationError error) => new(error);

    public static Validation operator +(Validation left, Validation right) {
        if (right.HasErrors)
            left.AddErrors(right.Errors);
        return left;
    }

    public static Validation operator +(Validation left, Success _) => left;
    public static Validation operator +(Validation left, ValidationError right) => left.AddErrors(new[] { right });
    public static Validation operator +(Validation left, ICollection<ValidationError> right) => left.AddErrors(right);
}