using static System.Constants.Constants.ErrorMessages;

namespace System.Validations;

public class StringValidationBuilder : IStringValidationConnector<IStringValidators>, IStringValidators {
    private readonly string? _subject;
    private readonly string _source;
    private readonly List<ValidationError> _errors = new();

    private StringValidationBuilder(string? subject, string? source) {
        _subject = subject;
        _source = source!;
    }

    public static StringValidationBuilder For(string? subject, [CallerArgumentExpression(nameof(subject))] string? source = null) => new(subject, source);

    public IStringValidationConnector<IStringValidators> Required {
        get {
            if (_subject is null)
                _errors.Add(new(string.Format(Null, _source), _source));
            return this;
        }
    }

    public IStringValidationConnector<IStringValidators> NotEmptyOrWhiteSpace {
        get {
            if (_subject is null) return this;
            if (_subject.Trim().Length == 0)
                _errors.Add(new(string.Format(EmptyOrWhitespace, _source), _source));
            return this;
        }
    }

    public IStringValidationConnector<IStringValidators> NoLongerThan(int maximumLength) {
        if ((_subject?.Length ?? 0) > maximumLength)
            _errors.Add(new(string.Format(LongerThan, _source, maximumLength), _source));
        return this;
    }

    public IStringValidators And => this;

    public ValidationResult Result => _errors.ToArray();
}