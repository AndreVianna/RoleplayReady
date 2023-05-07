namespace System.Validation.Builder;

public class TypeValidators : Validators<Type?>, ITypeValidators {
    private readonly Connectors<Type?, TypeValidators> _connector;
    private readonly ValidationCommandFactory _commandFactory;

    public TypeValidators(Type? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        _connector = new Connectors<Type?, TypeValidators>(this);
        _commandFactory = ValidationCommandFactory.For(typeof(Type), Source, Result);
    }

    public IConnectors<Type?, TypeValidators> IsEqualTo<TType>() {
        _commandFactory.Create(nameof(IsEqualToCommand<int>), typeof(TType)).Validate(Subject);
        return _connector;
    }
}