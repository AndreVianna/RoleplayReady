using static System.Validation.Builder.ValidatorMode;

namespace System.Validation.Builder;

public abstract class Validator(string source, ValidatorMode mode = And) : IValidator {
    public ValidatorMode Mode { get; private set; } = mode;
    public string Source { get; } = source;
    public ValidationResult Result { get; private set; } = ValidationResult.Success();

    public Validator SetMode(ValidatorMode mode) {
        Mode = mode;
        return this;
    }

    public void Negate() => Mode ^= ValidatorMode.Not;

    public void ClearErrors() => Result = ValidationResult.Success();

    public void AddError(ValidationError error) => Result += error;

    public void AddErrors(IEnumerable<ValidationError> errors) => Result += errors.ToArray();
}

public abstract class Validator<TSubject>(TSubject? subject, string source, ValidatorMode mode = And) : Validator(source, mode) {
    public TSubject? Subject { get; } = subject;

    protected void ValidateWith(IValidationCommand validator) {
        var rightResult = Mode.Has(Not)
            ? validator.Negate(Subject)
            : validator.Validate(Subject);
        if (Mode.Has(Or) && (rightResult.IsSuccess || Result.IsSuccess)) {
            ClearErrors();
            return;
        }

        AddErrors(rightResult.Errors);
    }
}