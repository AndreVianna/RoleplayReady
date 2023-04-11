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
            if (string.IsNullOrWhiteSpace(_subject))
                _errors.Add(new ValidationError(Constants.Constants.ErrorMessages.NullOrWhiteSpaces, _source));
            return this;
        }
    }

    public IStringValidationConnector<IStringValidators> NoLongerThan(int maximumLength) {
        if ((_subject?.Length ?? 0) > maximumLength)
            _errors.Add(new ValidationError(Constants.Constants.ErrorMessages.LongerThan, _source));
        return this;
    }

    public IStringValidationConnector<IStringValidators> NoShorterThan(int minimumLength) {
        if ((_subject?.Length ?? 0) < minimumLength)
            _errors.Add(new ValidationError(Constants.Constants.ErrorMessages.LongerThan, _source));
        return this;
    }

    public IStringValidators And => this;

    public ValidationError[] Errors => _errors.ToArray();
}