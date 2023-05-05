namespace System.Validation.Abstractions;

public interface ITypeValidators : IValidators<Type?, TypeValidators> {
    IValidatorsConnector<Type?, TypeValidators> IsEqualTo<TType>();
}