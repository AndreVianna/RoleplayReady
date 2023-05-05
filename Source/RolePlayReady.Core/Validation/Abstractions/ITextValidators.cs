namespace System.Validation.Abstractions;

public interface ITextValidators : IValidators<string?, TextValidators> {
    IValidatorsConnector<string?, TextValidators> IsNotEmptyOrWhiteSpace();
    IValidatorsConnector<string?, TextValidators> MinimumLengthIs(int length);
    IValidatorsConnector<string?, TextValidators> MaximumLengthIs(int length);
    IValidatorsConnector<string?, TextValidators> LengthIs(int length);
    IValidatorsConnector<string?, TextValidators> IsIn(params string[] list);
    IValidatorsConnector<string?, TextValidators> IsEmail();
    IValidatorsConnector<string?, TextValidators> IsPassword(IPasswordPolicy policy);
}