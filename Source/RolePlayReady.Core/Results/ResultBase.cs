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

    public override bool Equals(object? other) {
        if (other is null) return false;
        if (other is Success && IsSuccessful) return true;
        if (ReferenceEquals(this, other)) return true;
        if (other is not ResultBase<TValue> otherResult) return false;
        if (otherResult.IsSuccessful != IsSuccessful) return false;
        if (!IsSuccessful && Errors.Count != otherResult.Errors.Count) return false;
        if (!IsSuccessful && Errors.Zip(otherResult.Errors).Any(pair => !pair.First.Equals(pair.Second))) return false;
        return true;
    }

    public override int GetHashCode() {
        var hashCode = new HashCode();
        hashCode.Add(IsSuccessful);
        if (IsSuccessful) return hashCode.ToHashCode();
        foreach (var error in Errors) hashCode.Add(error);
        return hashCode.ToHashCode();
    }

    //public override bool Equals(Success? other) => other is not null && IsSuccessful;
}