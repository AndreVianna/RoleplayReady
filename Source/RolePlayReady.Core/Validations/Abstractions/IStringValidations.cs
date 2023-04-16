namespace System.Validations.Abstractions;

public interface IStringValidations : IValidations {
    IConnectors<IStringValidations> NotNull();
    IConnectors<IStringValidations> NotEmptyOrWhiteSpace();
    IConnectors<IStringValidations> MaximumLengthOf(int maximumLength);
    IConnectors<IStringValidations> MinimumLengthOf(int maximumLength);
}