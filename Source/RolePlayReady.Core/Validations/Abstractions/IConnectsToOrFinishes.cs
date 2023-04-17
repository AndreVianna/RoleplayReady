namespace System.Validations.Abstractions;

public interface IConnectsToOrFinishes<out TValidation>  : IConnectsTo<TValidation>, IFinishesValidation {
}