using Create = System.Utilities.Create;

namespace System.Validation.Builder;

public class Connectors<TSubject, TValidators> : IConnectors<TSubject, TValidators>
    where TValidators : Validators<TSubject> {
    private readonly TValidators _left;

    public Connectors(TValidators left) {
        _left = left;
    }

    public TValidators And() => _left;

    public TValidators And(Func<TSubject?, string, ValidationResult> validateRight) {
        var right = validateRight(_left.Subject, _left.Source);
        return Create.Instance<TValidators>(_left.Subject, _left.Source, _left.Result.Errors.Union(right.Errors));
    }

    public TValidators Or()
        => throw new NotImplementedException();
    public TValidators Not()
        => throw new NotImplementedException();

    public TValidators And(Func<TSubject, ITerminator> validateRight)
        => throw new NotImplementedException();
    public TValidators Or(Func<TSubject, ITerminator> validateRight)
        => throw new NotImplementedException();
    public IConnectors<TSubject, TValidators> Not(Func<TSubject, ITerminator> validateRight)
        => throw new NotImplementedException();

    public ValidationResult Result => _left.Result;
}