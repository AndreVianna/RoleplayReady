namespace System.Validation.Builder;

public class ObjectValidator : Validator<object?>, IObjectValidator {
    private readonly ValidationCommandFactory _commandFactory;

    public ObjectValidator(object? subject, string source, ValidatorMode mode = ValidatorMode.And)
        : base(subject, source, mode) {
        Connector = new Connector<object?, ObjectValidator>(this);
        _commandFactory = ValidationCommandFactory.For(typeof(int?), Source);
    }

    public Connector<object?, ObjectValidator> Connector { get; }

    public IConnector<ObjectValidator> IsNull() {
        Result += _commandFactory.Create(nameof(IsNull)).Validate(Subject);
        return Connector;
    }

    public IConnector<ObjectValidator> IsNotNull() {
        Result += _commandFactory.Create(nameof(IsNull)).Negate(Subject);
        return Connector;
    }
}