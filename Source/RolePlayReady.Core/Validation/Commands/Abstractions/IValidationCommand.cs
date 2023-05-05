namespace System.Validation.Commands.Abstractions;

public interface IValidationCommand {
    ValidationResult Validate();
    ValidationResult Negate();
}
