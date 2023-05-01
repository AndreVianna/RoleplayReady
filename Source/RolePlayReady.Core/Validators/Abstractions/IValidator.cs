namespace System.Validators.Abstractions;

public interface IValidator {
    ValidationResult Validate(object? value);
}
