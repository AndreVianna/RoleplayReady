using System.Validation.Abstractions;

namespace System.Validation;

public class DecimalValidators
    : Validators<decimal?, DecimalValidators>
        , IDecimalValidators {

    public static DecimalValidators CreateAsOptional(decimal? subject, string source)
        => new(subject, source);
    public static DecimalValidators CreateAsRequired(decimal? subject, string source)
        => new(subject, source, EnsureNotNull(subject, source));

    private DecimalValidators(decimal? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        Connector = new Connectors<decimal?, DecimalValidators>(Subject, this);
    }

    public IValidatorsConnector<decimal?, DecimalValidators> MinimumIs(decimal threshold) {
        CommandFactory.Create(nameof(MinimumIs), threshold).Validate();
        return Connector;
    }

    public IValidatorsConnector<decimal?, DecimalValidators> IsGreaterThan(decimal threshold) {
        CommandFactory.Create(nameof(IsGreaterThan), threshold).Validate();
        return Connector;
    }

    public IValidatorsConnector<decimal?, DecimalValidators> IsEqualTo(decimal threshold) {
        CommandFactory.Create(nameof(IsEqualTo), threshold).Validate();
        return Connector;
    }

    public IValidatorsConnector<decimal?, DecimalValidators> IsLessThan(decimal threshold) {
        CommandFactory.Create(nameof(IsLessThan), threshold).Validate();
        return Connector;
    }

    public IValidatorsConnector<decimal?, DecimalValidators> MaximumIs(decimal threshold) {
        CommandFactory.Create(nameof(MaximumIs), threshold).Validate();
        return Connector;
    }
}