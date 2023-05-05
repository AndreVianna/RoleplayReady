namespace System.Validation.Builder;

public class Connectors<TSubject, TValidators> : IConnectors<TSubject, TValidators>
    where TValidators : Validators<TSubject, TValidators> {
    private readonly TSubject? _subject;
    private readonly TValidators _left;

    public Connectors(TSubject? subject, TValidators left) {
        _subject = subject;
        _left = left;
    }

    public TValidators And() => _left;

    public TValidators And(Func<TSubject?, string, ValidationResult> validateRight) {
        var right = validateRight(_subject, _left.Source);
        return Create.Instance<TValidators>(_subject, _left.Source, _left.Result.Errors.Union(right.Errors));
    }

    public TValidators Or() => _left;
    public TValidators And(Func<TSubject?, ValidationResult> validateRight) => throw new NotImplementedException();

    public TValidators Or(Func<TSubject?, ValidationResult> validateRight) => throw new NotImplementedException();

    public TValidators Or(Func<TSubject?, string, ValidationResult> right) => throw new NotImplementedException();

    public ValidationResult Result => _left.Result;
}