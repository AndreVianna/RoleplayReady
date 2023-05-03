namespace System.Validations.Abstractions;

public interface IDateTimeValidation
    : IConnectsToOrFinishes<IDateTimeValidators>,
        IDateTimeValidators {
}