namespace System.Validation;

public interface IValidatable {
    ValidationResult ValidateSelf(bool negate = false);
}
