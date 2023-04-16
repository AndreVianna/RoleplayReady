namespace System.Validations.Abstractions;

public interface IIntegerValidations : IValidations {
    IConnectors<IIntegerValidations> LessOrEqualTo(int upperLimit);
    IConnectors<IIntegerValidations> GreaterOrEqualTo(int lowerLimit);
    IConnectors<IIntegerValidations> LessThan(int upperLimit);
    IConnectors<IIntegerValidations> GreaterThan(int lowerLimit);
}