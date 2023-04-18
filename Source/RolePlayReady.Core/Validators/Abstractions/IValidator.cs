namespace System.Validators.Abstractions;

public interface IValidator {
    ValidationResult Validate<TValue>(TValue value);
}
