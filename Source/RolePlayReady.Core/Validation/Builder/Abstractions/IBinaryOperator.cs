namespace System.Validation.Builder.Abstractions;

public interface IBinaryOperator<TValidator>
    where TValidator : IValidator {
    TValidator And(Func<TValidator, TValidator> validateRight);
    TValidator Or(Func<TValidator, TValidator> validateRight);
    TValidator AndNot(Func<TValidator, TValidator> validateRight);
    TValidator OrNot(Func<TValidator, TValidator> validateRight);
}