namespace System.Validations.Abstractions;

public interface IDecimalValidation
    : IFinishesValidation,
      IConnectsToValidation<IDecimalValidation> {
    IDecimalValidation LessOrEqualTo(decimal maximum);
    IDecimalValidation GreaterOrEqualTo(decimal minimum);
    IDecimalValidation LessThan(decimal maximum);
    IDecimalValidation GreaterThan(decimal minimum);
}