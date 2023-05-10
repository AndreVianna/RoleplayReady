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
        Result += _commandFactory.Create(nameof(IsNull)).Validate(Subject);
        return Connector;
    }

    public IConnector<DateTimeValidator> IsNotNull() {
        Result += _commandFactory.Create(nameof(IsNull)).Negate(Subject);
        return Connector;
    }

    public IConnector<DateTimeValidator> IsBefore(DateTime threshold) {
        Result += _commandFactory.Create(nameof(IsBefore), threshold).Validate(Subject);
        return Connector;
    }

    public IConnector<DateTimeValidator> StartsOn(DateTime threshold) {
        Result += _commandFactory.Create(nameof(IsBefore), threshold).Negate(Subject);
        return Connector;
    }

    public IConnector<DateTimeValidator> EndsOn(DateTime threshold) {
        Result += _commandFactory.Create(nameof(IsAfter), threshold).Negate(Subject);
        return Connector;
    }

    public IConnector<DateTimeValidator> IsAfter(DateTime threshold) {
        Result += _commandFactory.Create(nameof(IsAfter), threshold).Validate(Subject);
        return Connector;
    }
}