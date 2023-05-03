namespace System.Validations.Abstractions;

public interface ITextValidation
    : IConnectsToOrFinishes<ITextValidators>,
        ITextValidators {
}