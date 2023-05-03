namespace System.Validators.Abstractions;

public interface ITextValidators {
    IConnectsToOrFinishes<ITextValidators> IsNotEmptyOrWhiteSpace();
    IConnectsToOrFinishes<ITextValidators> MinimumLengthIs(int length);
    IConnectsToOrFinishes<ITextValidators> MaximumLengthIs(int length);
    IConnectsToOrFinishes<ITextValidators> LengthIs(int length);
    IConnectsToOrFinishes<ITextValidators> IsIn(params string[] list);
    IConnectsToOrFinishes<ITextValidators> IsEmail();
}