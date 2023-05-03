namespace System.Validators.Abstractions;

public interface IValidator {
    ValidationResult Validate(object? input, [CallerArgumentExpression(nameof(input))] string? source = null);
}
