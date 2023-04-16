namespace System.Validations.Abstractions;

public interface ICollectionValidations : IValidations {
    IConnectors<ICollectionValidations> NotEmpty();
}