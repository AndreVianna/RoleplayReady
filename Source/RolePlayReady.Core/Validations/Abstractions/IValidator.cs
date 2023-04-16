namespace System.Validations.Abstractions;

public interface IValidator {
    ValidationResult Validate(object? value);
}