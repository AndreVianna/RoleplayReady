namespace System.Validation.Builder;

public class TypeValidators : Validators<Type?>, ITypeValidators {
    private readonly Connectors<Type?, TypeValidators> _connector;
    private readonly ValidationCommandFactory<Type> _commandFactory;

    public static TypeValidators Create(Type? subject, string source)
        => new(subject, source, EnsureNotNull(subject, source));

    private TypeValidators(Type? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        _connector = new Connectors<Type?, TypeValidators>(this);
        _commandFactory = ValidationCommandFactory.For(Subject, Source, Result);
    }

    public IConnectors<Type?, TypeValidators> IsEqualTo<TType>() {
        _commandFactory.Create(nameof(IsEqualToCommand<int>), typeof(TType)).Validate();
        return _connector;
    }
}