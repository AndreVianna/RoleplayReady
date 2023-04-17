namespace System.Validations.Abstractions;

public interface IIntegerValidation
    : IConnectsToOrFinishes<IIntegerValidation> {
    IConnectsToOrFinishes<IIntegerValidation> LessOrEqualTo(int maximum);
    IConnectsToOrFinishes<IIntegerValidation> GreaterOrEqualTo(int minimum);
    IConnectsToOrFinishes<IIntegerValidation> LessThan(int maximum);
    IConnectsToOrFinishes<IIntegerValidation> GreaterThan(int minimum);
}