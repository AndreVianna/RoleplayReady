namespace System.Validations;

public class StringCollectionValidationBuilder : IStringValidationConnector<IStringCollectionValidators>, IStringCollectionValidators {
    private readonly IList<string>? _subject;
    private readonly string _source;
    private readonly List<ValidationError> _errors = new();

    private StringCollectionValidationBuilder(IList<string> subject, string? source) {
        _subject = subject;
        _source = source!;
    }

    public static StringCollectionValidationBuilder For(IList<string> subject, [CallerArgumentExpression(nameof(subject))] string? source = null) => new(subject, source);

    public IStringValidationConnector<IStringCollectionValidators> Required {
        get {
            if (_subject is null)
                _errors.Add(new(Constants.Constants.ErrorMessages.Null, _source));
            return this;
        }
    }

    public IStringValidationConnector<IStringCollectionValidators> AllAre(Func<IStringValidators, IStringValidationConnector<IStringValidators>> validate) {
        if (_subject is null) return this;
        for (var index = 0; index < _subject.Count; index++) {
            var validation = StringValidationBuilder.For(_subject[index], $"{_source}[{index}]");
            _errors.AddRange(validate(validation).Result.Errors);
        }

        return this;
    }

    public IStringCollectionValidators And => this;

    public ValidationResult Result => _errors.ToArray();
}