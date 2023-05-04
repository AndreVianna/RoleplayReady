namespace System.Validations;

public partial class TextValidations : ITextValidations {
    private readonly Connects<TextValidations> _connector;

    public TextValidations(string? subject, string source, IEnumerable<ValidationError>? previousErrors = null) {
        Subject = subject;
        Source = source;
        Errors = previousErrors?.ToList() ?? new List<ValidationError>();
        _connector = new Connects<TextValidations>(this);
    }

    public string? Subject { get; }
    public string Source { get; }
    public List<ValidationError> Errors { get; }

    public IConnects<ITextValidations> IsNotEmptyOrWhiteSpace() {
        if (Subject is not null && Subject.Trim().Length == 0)
            Errors.Add(new(CannotBeEmptyOrWhitespace, Source));
        return _connector;
    }

    public IConnects<ITextValidations> MinimumLengthIs(int length) {
        if (Subject is not null && Subject.Length < length)
            Errors.Add(new(ErrorMessages.MinimumLengthIs, Source, length, Subject.Length));

        return _connector;
    }

    public IConnects<ITextValidations> IsIn(params string?[] list) {
        if (Subject is not null && !list.Contains(Subject))
            Errors.Add(new(MustBeIn, Source, string.Join(", ", list), Subject));

        return _connector;
    }

    public IConnects<ITextValidations> LengthIs(int length) {
        if (Subject is not null && Subject.Length != length)
            Errors.Add(new(LengthMustBe, Source, length, Subject.Length));

        return _connector;
    }

    public IConnects<ITextValidations> MaximumLengthIs(int length) {
        if (Subject is not null && Subject.Length > length)
            Errors.Add(new(ErrorMessages.MaximumLengthIs, Source, length, Subject.Length));

        return _connector;
    }

    public IConnects<ITextValidations> IsEmail() {
        if (Subject is not null && !EmailChecker().IsMatch(Subject))
            Errors.Add(new(IsNotAValidEmail, Source));

        return _connector;
    }

    public IConnects<ITextValidations> IsPassword(IPasswordPolicy policy) {
        if (string.IsNullOrWhiteSpace(Subject) || policy.TryValidate(Subject, out var errors))
            return _connector;
        Errors.Add(new(IsNotAValidPassword, Source));
        foreach(var error in errors) Errors.Add(error);
        return _connector;
    }

    [GeneratedRegex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex EmailChecker();
}