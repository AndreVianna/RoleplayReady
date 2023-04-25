namespace System.Validators.Abstractions;

public interface IValidator {
    Result Validate(object? value);
}
