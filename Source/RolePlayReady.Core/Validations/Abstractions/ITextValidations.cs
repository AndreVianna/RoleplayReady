namespace System.Validations.Abstractions;

public interface ITextValidations : IValidations<string> {
    IConnects<ITextValidations> IsNotEmptyOrWhiteSpace();
    IConnects<ITextValidations> MinimumLengthIs(int length);
    IConnects<ITextValidations> MaximumLengthIs(int length);
    IConnects<ITextValidations> LengthIs(int length);
    IConnects<ITextValidations> IsIn(params string[] list);
    IConnects<ITextValidations> IsEmail();
    IConnects<ITextValidations> IsPassword(IPasswordPolicy policy);
}