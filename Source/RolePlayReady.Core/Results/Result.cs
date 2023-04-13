using System.Results.Abstractions;

namespace System.Results;

public class Result<TObject> : IResult<TObject> {
    private OneOf<TObject, Failure> _result;

    public Result() { }

    public Result(object? input) {
        _result = input switch {
            Result<TObject> result => result._result,
            ICollection<ValidationError> errors => new Failure(errors),
            ValidationError error => new Failure(error),
            TObject value => value,
            _ => throw new InvalidCastException(string.Format(ResultInvalidType)),
        };
    }

    public bool IsSuccess => _result.IsT0;
    public bool HasValue => _result.IsT0 && _result.AsT0 is not null;
    public bool HasErrors => _result.IsT1;

    [NotNull]
    public TObject Value => HasValue
        ? _result.AsT0!
        : throw new InvalidCastException(ResultHasNoValue);

    public ICollection<ValidationError> Errors
        => _result.IsT0
            ? NoErrors
            : _result.AsT1.Errors;

    protected Result<TObject> AddErrors(ICollection<ValidationError> errors) {
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

    public static implicit operator Result<TObject>(TObject? value) => new(value);
    public static implicit operator Result<TObject>(Failure failure) => new(failure.Errors);
    public static implicit operator Result<TObject>(List<ValidationError> errors) => new(errors);
    public static implicit operator Result<TObject>(ValidationError[] errors) => new(errors);
    public static implicit operator Result<TObject>(ValidationError error) => new(error);

    public static implicit operator TObject(Result<TObject> input) => input.Value;

    public static Result<TObject> operator +(Result<TObject> left, Validation right) {
        if (right.HasErrors)
            left.AddErrors(right.Errors);
        return left;
    }

    public static Result<TObject> operator +(Result<TObject> left, Success _) => left;
    public static Result<TObject> operator +(Result<TObject> left, ValidationError right) => left.AddErrors(new[] { right });
    public static Result<TObject> operator +(Result<TObject> left, ICollection<ValidationError> right) => left.AddErrors(right);
}