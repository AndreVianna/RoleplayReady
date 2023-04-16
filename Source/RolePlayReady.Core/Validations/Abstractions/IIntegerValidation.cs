namespace System.Validations.Abstractions;

public interface IIntegerValidation
    : IFinishesValidation,
      IConnectsToValidation<IIntegerValidation> {
    IIntegerValidation LessOrEqualTo(int maximum);
    IIntegerValidation GreaterOrEqualTo(int minimum);
    IIntegerValidation LessThan(int maximum);
    IIntegerValidation GreaterThan(int minimum);
}