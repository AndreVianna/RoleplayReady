namespace System.Validation.Builder.Abstractions;

public interface ITextValidators : IValidators<string?, TextValidators> {
    IConnectors<string?, TextValidators> IsNotEmptyOrWhiteSpace();
    IConnectors<string?, TextValidators> MinimumLengthIs(int length);
    IConnectors<string?, TextValidators> MaximumLengthIs(int length);
    IConnectors<string?, TextValidators> LengthIs(int length);
    IConnectors<string?, TextValidators> IsIn(params string[] list);
    IConnectors<string?, TextValidators> IsEmail();
    IConnectors<string?, TextValidators> IsPassword(IPasswordPolicy policy);
}