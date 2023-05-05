namespace System.Validation.Builder;

public class DateTimeValidators
    : Validators<DateTime?, DateTimeValidators>
        , IDateTimeValidators {
    public static DateTimeValidators CreateAsOptional(DateTime? subject, string source)
        => new(subject, source);
    public static DateTimeValidators CreateAsRequired(DateTime? subject, string source)
        => new(subject, source, EnsureNotNull(subject, source));

    private DateTimeValidators(DateTime? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        Connector = new Connectors<DateTime?, DateTimeValidators>(Subject, this);
    }

    public IConnectors<DateTime?, DateTimeValidators> IsBefore(DateTime threshold) {
        CommandFactory.Create(nameof(IsBefore), threshold).Validate();
        return Connector;
    }

    public IConnectors<DateTime?, DateTimeValidators> StartsOn(DateTime threshold) {
        CommandFactory.Create(nameof(StartsOn), threshold).Validate();
        return Connector;
    }

    public IConnectors<DateTime?, DateTimeValidators> EndsOn(DateTime threshold) {
        CommandFactory.Create(nameof(EndsOn), threshold).Validate();
        return Connector;
    }

    public IConnectors<DateTime?, DateTimeValidators> IsAfter(DateTime threshold) {
        CommandFactory.Create(nameof(IsAfter), threshold).Validate();
        return Connector;
    }
}