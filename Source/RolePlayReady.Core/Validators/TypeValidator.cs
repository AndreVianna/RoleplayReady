namespace System.Validators;

public class TypeValidator
    : Validator<Type, ITypeChecks, ITypeConnectors>,
        ITypeChecks,
        ITypeConnectors {

    public TypeValidator(Type subject, string? source)
        : base(subject, source) {
    }
}