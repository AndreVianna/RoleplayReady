namespace System.Validation.Builder;

public class DateTimeValidator : Validator<DateTime?>, IDateTimeValidator {
    private readonly ValidationCommandFactory _commandFactory;

    public DateTimeValidator(DateTime? subject, string source, ValidatorMode mode = ValidatorMode.And)
        : base(subject, source, mode) {
        Connector = new Connector<DateTime?, DateTimeValidator>(this);
        _commandFactory = ValidationCommandFactory.For(typeof(DateTime?), Source);
    }

    public IConnector<DateTimeValidator> Connector { get; }

    public IConnector<DateTimeValidator> IsNull() {
        var validator = _commandFactory.Create(nameof(IsNull));
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<DateTimeValidator> IsNotNull() {
        var validator = _commandFactory.Create(nameof(IsNull));
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<DateTimeValidator> IsBefore(DateTime threshold) {
        var validator = _commandFactory.Create(nameof(IsBefore), threshold);
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<DateTimeValidator> StartsOn(DateTime threshold) {
        var validator = _commandFactory.Create(nameof(IsBefore), threshold);
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<DateTimeValidator> EndsOn(DateTime threshold) {
        var validator = _commandFactory.Create(nameof(IsAfter), threshold);
        ValidateWith(validator);
        return Connector;
    }

    public IConnector<DateTimeValidator> IsAfter(DateTime threshold) {
        var validator = _commandFactory.Create(nameof(IsAfter), threshold);
        ValidateWith(validator);
        return Connector;
    }
}