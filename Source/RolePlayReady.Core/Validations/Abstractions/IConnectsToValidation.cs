namespace System.Validations.Abstractions;

public interface IConnectsToValidation<out TValidation>
    where TValidation : class, IFinishesValidation {
    TValidation And { get; }
}