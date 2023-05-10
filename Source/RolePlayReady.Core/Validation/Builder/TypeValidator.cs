namespace System.Validation.Builder;

public class TypeValidator : Validator<Type?>, ITypeValidator {
    private readonly ValidationCommandFactory _commandFactory;

    public TypeValidator(Type? subject, string source, ValidatorMode mode = ValidatorMode.And)
        : base(subject, source, mode) {
        Connector1 = new Connector<Type?, TypeValidator>(this);
        _commandFactory = ValidationCommandFactory.For(typeof(Type), Source);
    }

    public IConnector<TypeValidator> Connector1 { get; }

    public IConnector<TypeValidator> IsNull() {
        Result += _commandFactory.Create(nameof(IsNull)).Validate(Subject);
        return Connector1;
    }

    public IConnector<TypeValidator> IsNotNull() {
        Result += _commandFactory.Create(nameof(IsNull)).Negate(Subject);
        return Connector1;
    }

    public IConnector<TypeValidator> IsEqualTo<TType>() {
        Result += _commandFactory.Create(nameof(IsEqualTo), typeof(TType)).Validate(Subject);
        return Connector1;
    }
}