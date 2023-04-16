namespace System.Validations.Abstractions;

public interface IValidatableTypeValidation
    : IFinishesValidation,
      IConnectsToValidation<IValidatableTypeValidation> {
    IValidatableTypeValidation NotNull();
    IFinishesValidation Valid();
}