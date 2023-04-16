namespace System.Validations.Abstractions;

public interface INullOrDecimalValidations : IValidations {
    IConnectors<IDecimalValidations> NotNull();
}