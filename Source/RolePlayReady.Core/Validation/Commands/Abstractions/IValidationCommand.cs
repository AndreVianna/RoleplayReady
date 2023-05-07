namespace System.Validation.Commands.Abstractions;

public interface IValidationCommand {
    ValidationResult Validate(object? subject);
    ValidationResult Negate(object? subject);
}
