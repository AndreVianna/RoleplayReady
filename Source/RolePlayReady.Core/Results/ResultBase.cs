using IValidation = System.Results.Abstractions.IValidation;

namespace System.Results;

public abstract class ResultBase<TValue> : IValidation {
    private Failure? _failure;

    protected ResultBase(object? input) {
        (InternalValue, _failure) = input switch {
            ValidationResult validation => (default, new(validation.Errors)),
            ICollection<ValidationError> errors => (default, new(errors)),
            ValidationError error => (default, new(error)),
            TValue value => (value, default),
            _ => (default(TValue?), default(Failure?)),
        };
    }

    public bool IsSuccessful => _failure is null;
    public bool HasErrors => !IsSuccessful;
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

    public static bool operator !=(ResultBase<TValue> left, SuccessfulResult _) => !left.IsSuccessful;
    public static bool operator ==(ResultBase<TValue> left, SuccessfulResult _) => left.IsSuccessful;
}