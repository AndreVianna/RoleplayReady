namespace System.Validators;

public class ComplexValidator<TComplex>
    : Validator<TComplex?, IComplexChecks, IComplexConnectors>,
        IComplexChecks,
        IComplexConnectors
    where TComplex : IValidatable {

    public ComplexValidator(TComplex? subject, string? source)
        : base(subject, source) {
    }

    public IComplexConnectors Valid() {
        if (Subject is not IValidatable validatable)
            return this;
        foreach (var error in validatable.Validate().Errors) {
            error.Arguments[0] = $"{Source}.{error.Arguments[0]}";
            Errors.Add(error);
        }

        return this;
    }
}
