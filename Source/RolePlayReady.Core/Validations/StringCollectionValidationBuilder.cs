namespace System.Validations;

public class StringCollectionValidationBuilder : IStringValidationConnector<IStringCollectionValidators>, IStringCollectionValidators {
    private readonly IList<string> _subject;
    private readonly string _source;
    private readonly List<ValidationError> _errors = new();

    private StringCollectionValidationBuilder(IList<string> subject, string? source) {
        _subject = subject;
        _source = source!;
    }

    public static StringCollectionValidationBuilder For(IList<string> subject, [CallerArgumentExpression(nameof(subject))] string? source = null) => new(subject, source);

    public IStringValidationConnector<IStringCollectionValidators> Required {
        get {
            if (_subject.Any())
                _errors.Add(new ValidationError(Constants.Constants.ErrorMessages.NullOrEmpty, _source));
            return this;
        }
    }

    public IStringValidationConnector<IStringCollectionValidators> AreAll(Func<IStringValidators, IStringValidationConnector<IStringValidators>> validate) {
        for (var index = 0; index < _subject.Count; index++) {
            var validation = StringValidationBuilder.For(_subject[index], $"{_source}[{index}]");
            _errors.AddRange(validate(validation).Errors);
        }

        return this;
    }

    public IStringCollectionValidators And => this;

    public ValidationError[] Errors => _errors.ToArray();
}