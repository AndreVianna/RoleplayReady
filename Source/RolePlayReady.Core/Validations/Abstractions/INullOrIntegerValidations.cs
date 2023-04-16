namespace System.Validations.Abstractions;

public interface INullOrIntegerValidations : IValidations {
    IConnectors<IIntegerValidations> NotNull();
}