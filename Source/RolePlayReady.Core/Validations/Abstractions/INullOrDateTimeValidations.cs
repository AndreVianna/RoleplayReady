namespace System.Validations.Abstractions;

public interface INullOrDateTimeValidations : IValidations {
    IConnectors<IDateTimeValidations> NotNull();
}