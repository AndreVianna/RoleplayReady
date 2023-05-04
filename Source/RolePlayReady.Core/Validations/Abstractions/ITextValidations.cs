namespace System.Validations.Abstractions;

public interface ITextValidations : IValidations<string?, TextValidations> {
    IValidationsConnector<string?, TextValidations> IsNotEmptyOrWhiteSpace();
    IValidationsConnector<string?, TextValidations> MinimumLengthIs(int length);
    IValidationsConnector<string?, TextValidations> MaximumLengthIs(int length);
    IValidationsConnector<string?, TextValidations> LengthIs(int length);
    IValidationsConnector<string?, TextValidations> IsIn(params string[] list);
    IValidationsConnector<string?, TextValidations> IsEmail();
    IValidationsConnector<string?, TextValidations> IsPassword(IPasswordPolicy policy);
}