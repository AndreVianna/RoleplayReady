namespace System.Validations.Abstractions;

public interface IDecimalValidation
    : IConnectsToOrFinishes<IDecimalValidation> {
    IConnectsToOrFinishes<IDecimalValidation> LessOrEqualTo(decimal maximum);
    IConnectsToOrFinishes<IDecimalValidation> GreaterOrEqualTo(decimal minimum);
    IConnectsToOrFinishes<IDecimalValidation> LessThan(decimal maximum);
    IConnectsToOrFinishes<IDecimalValidation> GreaterThan(decimal minimum);
}