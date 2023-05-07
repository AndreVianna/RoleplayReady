namespace System.Validation.Builder;

public class DateTimeValidators : Validators<DateTime?>, IDateTimeValidators {
    private readonly Connectors<DateTime?, DateTimeValidators> _connector;
    private readonly ValidationCommandFactory _commandFactory;

    public DateTimeValidators(DateTime? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        _connector = new Connectors<DateTime?, DateTimeValidators>(this);
        _commandFactory = ValidationCommandFactory.For(typeof(DateTime?), Source, Result);
    }

    public IConnectors<DateTime?, DateTimeValidators> IsBefore(DateTime threshold) {
        _commandFactory.Create(nameof(IsBeforeCommand), threshold).Validate(Subject);
        return _connector;
    }

    public IConnectors<DateTime?, DateTimeValidators> StartsOn(DateTime threshold) {
        _commandFactory.Create(nameof(IsBeforeCommand), threshold).Negate(Subject);
        return _connector;
    }

    public IConnectors<DateTime?, DateTimeValidators> EndsOn(DateTime threshold) {
        _commandFactory.Create(nameof(IsAfterCommand), threshold).Negate(Subject);
        return _connector;
    }

    public IConnectors<DateTime?, DateTimeValidators> IsAfter(DateTime threshold) {
        _commandFactory.Create(nameof(IsAfterCommand), threshold).Validate(Subject);
        return _connector;
    }
}