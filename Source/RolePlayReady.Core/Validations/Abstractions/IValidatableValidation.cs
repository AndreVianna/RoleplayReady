namespace System.Validations.Abstractions;

public interface IValidatableValidation
    : IConnectsToOrFinishes<IValidatableValidation> {
    IFinishesValidation Valid();
}