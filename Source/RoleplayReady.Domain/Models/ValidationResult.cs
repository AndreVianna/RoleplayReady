namespace RoleplayReady.Domain.Models;

public readonly struct ValidationResult
{
    private readonly OneOf<Valid, Invalid> _validationResult;
    private static readonly ReadOnlyCollection<ValidationError> _empty = new(Array.Empty<ValidationError>());

    public ValidationResult()
    {
        _validationResult = new Valid();
    }

    private ValidationResult(IEnumerable<ValidationError?>? errors)
    {
        _validationResult = new Invalid(errors);
    }

    private ValidationResult(ValidationError? error)
    {
        _validationResult = new Invalid(error);
    }

    private ValidationResult AddErrors(IEnumerable<ValidationError?>? errors)
    {
        var validationErrors = errors as ValidationError?[] ?? errors?.ToArray() ?? Array.Empty<ValidationError?>();
        if (validationErrors.Length == 0)
        {
            throw new ArgumentException("The error collection cannot be null or empty.", nameof(errors));
        }

        if (validationErrors.Any(e => e is null))
        {
            throw new ArgumentException("The error collection cannot contain null elements.", nameof(errors));
        }

        return new(_validationResult.IsT1 ? _validationResult.AsT1.Errors.Concat(validationErrors) : validationErrors);
    }

    private ValidationResult AddError(ValidationError? error) => AddErrors(new[] { error ?? throw new ArgumentException("The error cannot be null.", nameof(error)) });

    public bool HasErrors => _validationResult.IsT1;
    public IEnumerable<ValidationError> Errors => _validationResult.IsT1 ? _validationResult.AsT1.Errors : _empty;
    public bool TryGetErrors(out IEnumerable<ValidationError>? errors)
    {
        errors = _validationResult.IsT1 ? _validationResult.AsT1.Errors : null;
        return _validationResult.IsT1;
    }

    public static implicit operator ValidationResult(Valid _) => new();
    public static implicit operator ValidationResult(Invalid invalid) => new(invalid.Errors);
    public static implicit operator ValidationResult(List<ValidationError?> errors) => new(errors);
    public static implicit operator ValidationResult(ValidationError?[] errors) => new(errors);
    public static implicit operator ValidationResult(ValidationError error) => new(error);

    public static ValidationResult operator +(ValidationResult left, ValidationError? right) => left.AddError(right);
    public static ValidationResult operator +(ValidationResult left, IEnumerable<ValidationError?> right) => left.AddErrors(right);
}