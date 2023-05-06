namespace System.Validation.Builder;

public class DateTimeValidators : Validators<DateTime?>, IDateTimeValidators {
    private readonly Connectors<DateTime?, DateTimeValidators> _connector;
    private readonly ValidationCommandFactory<DateTime?> _commandFactory;

    public static DateTimeValidators CreateAsOptional(DateTime? subject, string source)
        => new(subject, source);
    public static DateTimeValidators CreateAsRequired(DateTime? subject, string source)
        => new(subject, source, EnsureNotNull(subject, source));

    private DateTimeValidators(DateTime? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        _connector = new Connectors<DateTime?, DateTimeValidators>(this);
        _commandFactory = ValidationCommandFactory.For(Subject, Source, Result);
    }

    public IConnectors<DateTime?, DateTimeValidators> IsBefore(DateTime threshold) {
        _commandFactory.Create(nameof(IsBeforeCommand), threshold).Validate();
        return _connector;
    }

    public IConnectors<DateTime?, DateTimeValidators> StartsOn(DateTime threshold) {
        _commandFactory.Create(nameof(IsBeforeCommand), threshold).Negate();
        return _connector;
    }

    public IConnectors<DateTime?, DateTimeValidators> EndsOn(DateTime threshold) {
        _commandFactory.Create(nameof(IsAfterCommand), threshold).Negate();
        return _connector;
    }

    public IConnectors<DateTime?, DateTimeValidators> IsAfter(DateTime threshold) {
        _commandFactory.Create(nameof(IsAfterCommand), threshold).Validate();
        return _connector;
    }
}