namespace System.Results;

public readonly struct ValidationResult {
    private readonly OneOf<Valid, Invalid> _result;
    private static readonly ReadOnlyCollection<ValidationError> _noErrors = new(Array.Empty<ValidationError>());

    public ValidationResult() {
        _result = ResultFactory.Valid;
    }

    private ValidationResult(ICollection<ValidationError> errors) {
        _result = Ensure.NotNullOrHasNull(errors).Any() ? ResultFactory.Invalid(errors) : ResultFactory.Valid;
    }

    private ValidationResult(ValidationError? error) {
        _result = ResultFactory.Invalid(error);
    }

    private ValidationResult AddErrors(ICollection<ValidationError> errors) {
        var validationErrors = Ensure.NotNullOrHasNull(errors);
        return validationErrors.Any()
            ? _result.IsT1
                ? new(_result.AsT1.Errors.Concat(validationErrors).ToArray())
                : new(validationErrors)
            : this;
    }

    public static ValidationResult Valid => new();

    private ValidationResult AddError(ValidationError? error)
        => AddErrors(new[] { Ensure.NotNull(error) });

    public bool IsValid => _result.IsT0;
    public bool HasErrors => _result.IsT1;

    public ICollection<ValidationError> Errors => _result.IsT1 ? _result.AsT1.Errors : _noErrors;
    
    public bool TryGetErrors(out IEnumerable<ValidationError>? errors) {
        errors = _result.IsT1 ? _result.AsT1.Errors : null;
        return _result.IsT1;
    }

    public static implicit operator ValidationResult(Valid _) => new();
    public static implicit operator ValidationResult(Invalid invalid) => new(invalid.Errors);
    public static implicit operator ValidationResult(List<ValidationError> errors) => new(errors);
    public static implicit operator ValidationResult(ValidationError[] errors) => new(errors);
    public static implicit operator ValidationResult(ValidationError error) => new(error);

    public static ValidationResult operator +(ValidationResult left, Valid _) => left;
    public static ValidationResult operator +(ValidationResult left, ValidationError? right) => left.AddError(right);
    public static ValidationResult operator +(ValidationResult left, ValidationResult right) => left.AddErrors(right.Errors);
}