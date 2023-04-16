namespace System.Validations.Abstractions;

public interface IDecimalValidations : IValidations {
    IConnectors<IDecimalValidations> LessOrEqualTo(decimal upperLimit);
    IConnectors<IDecimalValidations> GreaterOrEqualTo(decimal lowerLimit);
    IConnectors<IDecimalValidations> LessThan(decimal upperLimit);
    IConnectors<IDecimalValidations> GreaterThan(decimal lowerLimit);
}