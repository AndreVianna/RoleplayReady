namespace System.Validation.Builder;

public class ObjectValidators : Validators<object?>, IObjectValidators {
    private readonly Connectors<object?, ObjectValidators> _connector;
    private readonly ValidationCommandFactory<object> _commandFactory;

    public static ObjectValidators CreateAsOptional(object? subject, string source)
        => new(subject, source);
    public static ObjectValidators CreateAsRequired(object? subject, string source)
        => new(subject, source, EnsureNotNull(subject, source));

    private ObjectValidators(object? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        _connector = new Connectors<object?, ObjectValidators>(this);
        _commandFactory = ValidationCommandFactory.For(Subject, Source, Result);
    }

    public IConnectors<object?, IValidators> IsOfType<TSubject>() {
        // TODO -- This is a hack to get around the fact that the command factory is not generic
        _commandFactory.Create(nameof(NullCommand)).Validate();
        return _connector;
    }
}