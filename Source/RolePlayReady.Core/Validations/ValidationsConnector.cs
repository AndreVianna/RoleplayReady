namespace System.Validations;

public class ValidationsConnector<TSubject, TValidations> : IValidationsConnector<TSubject, TValidations>
    where TValidations : Validations<TSubject, TValidations> {
    private readonly TSubject? _subject;
    private readonly TValidations _left;

    public ValidationsConnector(TSubject? subject, TValidations left) {
        _subject = subject;
        _left = left;
    }

    public TValidations And() => _left;

    public TValidations And(Func<TSubject?, string, ValidationResult> validateRight) {
        var right = validateRight(_subject, _left.Source);
        return Create.Instance<TValidations>(_subject, _left.Source, _left.Errors.Union(right.Errors));
    }

    public TValidations Or() => _left;
    public TValidations And(Func<TSubject?, ValidationResult> validateRight) => throw new NotImplementedException();

    public TValidations Or(Func<TSubject?, ValidationResult> validateRight) => throw new NotImplementedException();

    public TValidations Or(Func<TSubject?, string, ValidationResult> right) => throw new NotImplementedException();


    public ValidationResult Result => And().Errors;
}