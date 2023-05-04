namespace System.Validations.Abstractions;

public interface ITypeValidations : IValidations<Type?, TypeValidations> {
    IValidationsConnector<Type?, TypeValidations> IsEqualTo<TType>();
}