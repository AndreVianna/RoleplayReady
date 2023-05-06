namespace System.Validation.Builder;

public class DecimalValidators : Validators<decimal?>, IDecimalValidators {
    private readonly Connectors<decimal?, DecimalValidators> _connector;
    private readonly ValidationCommandFactory<decimal?> _commandFactory;

    public static DecimalValidators CreateAsOptional(decimal? subject, string source)
        => new(subject, source);
    public static DecimalValidators CreateAsRequired(decimal? subject, string source)
        => new(subject, source, EnsureNotNull(subject, source));

    private DecimalValidators(decimal? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        _connector = new Connectors<decimal?, DecimalValidators>(this);
        _commandFactory = ValidationCommandFactory.For(Subject, Source, Result);
    }

    public IConnectors<decimal?, DecimalValidators> MinimumIs(decimal threshold) {
        _commandFactory.Create(nameof(IsLessThanCommand<int>), threshold).Negate();
        return _connector;
    }

    public IConnectors<decimal?, DecimalValidators> IsGreaterThan(decimal threshold) {
        _commandFactory.Create(nameof(IsGreaterThanCommand<int>), threshold).Validate();
        return _connector;
    }

    public IConnectors<decimal?, DecimalValidators> IsEqualTo(decimal threshold) {
        _commandFactory.Create(nameof(IsEqualToCommand<int>), threshold).Validate();
        return _connector;
    }

    public IConnectors<decimal?, DecimalValidators> IsLessThan(decimal threshold) {
        _commandFactory.Create(nameof(IsLessThanCommand<int>), threshold).Validate();
        return _connector;
    }

    public IConnectors<decimal?, DecimalValidators> MaximumIs(decimal threshold) {
        _commandFactory.Create(nameof(IsGreaterThanCommand<int>), threshold).Negate();
        return _connector;
    }
}