using Create = System.Utilities.Create;

namespace System.Validation.Builder;

public class Connector<TSubject, TValidator>
    : IConnector<TValidator>
    where TValidator : Validator<TSubject> {
    private readonly TValidator _left;

    public Connector(TValidator left) {
        _left = left;
    }

    public ValidationResult Result => _left.Result;

    public TValidator And() => (TValidator)_left.SetMode(ValidatorMode.And);
    public TValidator Or() => (TValidator)_left.SetMode(ValidatorMode.Or);
    public TValidator AndNot() => (TValidator)_left.SetMode(ValidatorMode.AndNot);
    public TValidator OrNot() => (TValidator)_left.SetMode(ValidatorMode.OrNot);

    public TValidator And(Func<TValidator, TValidator> validateRight)
        => ProcessAnd(validateRight, ValidatorMode.And);

    public TValidator AndNot(Func<TValidator, TValidator> validateRight)
        => ProcessAnd(validateRight, ValidatorMode.AndNot);

    public TValidator Or(Func<TValidator, TValidator> validateRight)
        => ProcessOr(validateRight, ValidatorMode.And);

    public TValidator OrNot(Func<TValidator, TValidator> validateRight)
        => ProcessOr(validateRight, ValidatorMode.AndNot);

    private TValidator ProcessAnd(Func<TValidator, TValidator> validateRight, ValidatorMode mode) {
        var rightValidator = Create.Instance<TValidator>(_left.Subject, _left.Source, mode);
        rightValidator = validateRight(rightValidator);
        var combinedErrors = _left.Result.Errors.Union(rightValidator.Result.Errors);
        return Create.Instance<TValidator>(_left.Subject, _left.Source, _left.Mode, combinedErrors);
    }

    private TValidator ProcessOr(Func<TValidator, TValidator> validateRight, ValidatorMode mode) {
        var rightValidator = Create.Instance<TValidator>(_left.Subject, _left.Source, mode);
        rightValidator = validateRight(rightValidator);
        var combinedErrors = _left.Result.IsInvalid && rightValidator.Result.IsInvalid
            ? _left.Result.Errors.Union(rightValidator.Result.Errors)
            : Array.Empty<ValidationError>();
        return Create.Instance<TValidator>(_left.Subject, _left.Source, _left.Mode, combinedErrors);
    }
}