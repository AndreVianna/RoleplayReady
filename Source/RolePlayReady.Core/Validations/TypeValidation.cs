namespace System.Validations;

public class TypeValidation
    : Validation<Type, ITypeValidators>,
      ITypeValidation {

    public TypeValidation(Type? subject, string? source, IEnumerable<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
        }

    public IConnectsToOrFinishes<ITypeValidators> IsEqualTo(Type otherType) {
        if (Subject is null) return this;
        if (Subject != otherType)
            Errors.Add(new(IsNotEqual, Source, otherType, Subject));
        return this;
    }
}