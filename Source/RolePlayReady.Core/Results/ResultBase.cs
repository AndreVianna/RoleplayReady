using System.Results.Abstractions;

namespace System.Results;

public abstract class ResultBase<TValue> : IValidation {
    private Failure? _failure;

    protected ResultBase(object? input) {
        (InternalValue, _failure) = input switch {
            Validation validation => (default, new(validation.Errors)),
            ICollection<ValidationError> errors => (default, new(errors)),
            ValidationError error => (default, new(error)),
            TValue value => (value, default),
            _ => (default(TValue?), default(Failure?)),
        };
    }

    public bool IsValid => _failure is null;
    public bool HasErrors => !IsValid;
    protected TValue? InternalValue { get; }

    public ICollection<ValidationError> Errors => _failure?.Errors ?? new List<ValidationError>();

    protected IValidation AddErrors(IEnumerable<ValidationError> validationErrors) {
        var errors = Ensure.NotNullOrHasNull(validationErrors).ToArray();
        if (errors.Length == 0)
            return this;

        if (_failure is null) {
            _failure = new Failure(errors);
            return this;
        }

        foreach (var error in errors)
            _failure.Errors.Add(error);
        return this;
    }
}