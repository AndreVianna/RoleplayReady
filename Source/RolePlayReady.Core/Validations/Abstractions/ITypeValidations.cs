namespace System.Validations.Abstractions;

public interface ITypeValidations : IValidations<Type> {
    IConnects<ITypeValidations> IsEqualTo<TType>();
}