using System.Validation.Abstractions;

namespace System.Validation;

public class TypeValidators
    : Validators<Type?, TypeValidators>
        , ITypeValidators {
    public static TypeValidators CreateAsRequired(Type? subject, string source)
        => new(subject, source, EnsureNotNull(subject, source));

    private TypeValidators(Type? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        Connector = new Connectors<Type?, TypeValidators>(Subject, this);
    }

    public IValidatorsConnector<Type?, TypeValidators> IsEqualTo<TType>() {
        CommandFactory.Create(nameof(IsEqualTo), typeof(TType)).Validate();
        return Connector;
    }
}