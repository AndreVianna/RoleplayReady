namespace System.Validation.Builder.Abstractions;

public interface IValidator {
    ValidatorMode Mode { get; }
    string Source { get; }
    ValidationResult Result { get; }

    Validator SetMode(ValidatorMode mode);
}
