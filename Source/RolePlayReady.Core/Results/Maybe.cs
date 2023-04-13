using System.Results.Abstractions;

namespace System.Results;

public class Maybe<TObject> : INaybe<TObject> {
    private OneOf<TObject?, Failure> _result;

    public Maybe() { _result = default; }

    public Maybe(object? input) {
        _result = input switch {
            null => default,
            Maybe<TObject> result => result._result,
            ICollection<ValidationError> errors => new Failure(errors),
            ValidationError error => new Failure(error),
            TObject value => value,
            _ => throw new InvalidCastException(string.Format(ResultInvalidType)),
        };
    }

    public bool IsSuccess => _result.IsT0;
    public bool HasValue => _result.IsT0 && _result.AsT0 is not null;
    public bool IsNull => _result.IsT0 && _result.AsT0 is null;
    public bool HasErrors => _result.IsT1;

    [NotNull]
    public TObject Value => HasValue
        ? _result.AsT0!
        : throw new InvalidCastException(ResultHasNoValue);

    public TObject? Default => IsNull
        ? default
        : throw new InvalidCastException(ResultIsNotNull);

    public ICollection<ValidationError> Errors
        => IsSuccess
            ? NoErrors
            : _result.AsT1.Errors;

    protected Maybe<TObject> AddErrors(ICollection<ValidationError> errors) {
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

    public static implicit operator Maybe<TObject>(TObject? value) => new(value);
    public static implicit operator Maybe<TObject>(Failure failure) => new(failure.Errors);
    public static implicit operator Maybe<TObject>(List<ValidationError> errors) => new(errors);
    public static implicit operator Maybe<TObject>(ValidationError[] errors) => new(errors);
    public static implicit operator Maybe<TObject>(ValidationError error) => new(error);

    public static implicit operator TObject?(Maybe<TObject> input) => input.HasValue ? input.Value : input.Default;

    public static Maybe<TObject> operator +(Maybe<TObject> left, Validation right) {
        if (right.HasErrors)
            left.AddErrors(right.Errors);
        return left;
    }

    public static Maybe<TObject> operator +(Maybe<TObject> left, Success _) => left;
    public static Maybe<TObject> operator +(Maybe<TObject> left, ValidationError right) => left.AddErrors(new[] { right });
    public static Maybe<TObject> operator +(Maybe<TObject> left, ICollection<ValidationError> right) => left.AddErrors(right);
}