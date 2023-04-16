namespace System.Validations.Abstractions;

public interface IDictionaryValidations : IValidations {
    IConnectors<IDictionaryValidations> NotEmpty();
}