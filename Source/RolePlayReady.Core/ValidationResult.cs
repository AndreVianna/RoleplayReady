namespace System;

public readonly struct ValidationResult {
    private readonly OneOf<Valid, Invalid> _result;
    private static readonly ReadOnlyCollection<ValidationError> _noErrors = new(Array.Empty<ValidationError>());

    public ValidationResult() {
        _result = ResultFactory.Valid;
    }

    private ValidationResult(IEnumerable<ValidationError?>? errors) {
        _result = ResultFactory.Invalid(errors);
    }

    private ValidationResult(ValidationError? error) {
        _result = ResultFactory.Invalid(error);
    }

    private ValidationResult AddErrors(IEnumerable<ValidationError?>? errors) {
        var validationErrors = Throw.IfNullOrEmptyOrContainNulls(errors);
        return new(_result.IsT1
                    ? _result.AsT1.Errors.Concat(validationErrors).ToArray()
                    : validationErrors);
    }

    public static ValidationResult Valid => new();

    private ValidationResult AddError(ValidationError? error)
        => AddErrors(new[] { Throw.IfNull(error) });

    public bool IsValid => _result.IsT0;
    public bool HasErrors => _result.IsT1;
    public IEnumerable<ValidationError> Errors => _result.IsT1 ? _result.AsT1.Errors : _noErrors;
    public bool TryGetErrors(out IEnumerable<ValidationError>? errors) {
        errors = _result.IsT1 ? _result.AsT1.Errors : null;
        return _result.IsT1;
    }

    public static implicit operator ValidationResult(Valid _) => new();
    public static implicit operator ValidationResult(Invalid invalid) => new(invalid.Errors);
    public static implicit operator ValidationResult(List<ValidationError?> errors) => new(errors);
    public static implicit operator ValidationResult(ValidationError?[] errors) => new(errors);
    public static implicit operator ValidationResult(ValidationError error) => new(error);

    public static ValidationResult operator +(ValidationResult left, ValidationError? right) => left.AddError(right);
    public static ValidationResult operator +(ValidationResult left, ValidationResult right) => left.AddErrors(right.Errors);
}