namespace System.Validations;

public interface IStringValidators {
    IStringValidationConnector<IStringValidators> Required { get; }
    IStringValidationConnector<IStringValidators> NoLongerThan(int maximumLength);
    IStringValidationConnector<IStringValidators> NoShorterThan(int minimumLength);
}