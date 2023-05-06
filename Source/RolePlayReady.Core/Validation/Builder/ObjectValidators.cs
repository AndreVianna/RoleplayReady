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

    //public IConnects<TSubject> IsOfType<TSubject>() {
    //    if (Subject is not TSubject)
    //        Errors.Add(new(IsNotOfType, Source, typeof(TSubject), Source.GetType().Name));
    //    var value = Subject is TSubject typedValue ? typedValue : default;
    //    return new Connects<ValidationCommand<TSubject>>(value, Errors)!;
    //}
}