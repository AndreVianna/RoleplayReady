namespace System.Validations.Abstractions;

public interface IStringValidators {
    IStringValidationConnector<IStringValidators> Required { get; }
    IStringValidationConnector<IStringValidators> NotEmptyOrWhiteSpace { get; }
    IStringValidationConnector<IStringValidators> NoLongerThan(int maximumLength);
}