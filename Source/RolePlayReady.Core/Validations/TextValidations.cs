namespace System.Validations;

public partial class TextValidations
    : Validations<string?, TextValidations>
        , ITextValidations {

    public static TextValidations CreateAsOptional(string? subject, string source)
        => new(subject, source);
    public static TextValidations CreateAsRequired(string? subject, string source)
        => new(subject, source, EnsureNotNull(subject, source));

    private TextValidations(string? subject, string source, IEnumerable<ValidationError>? previousErrors = null)
        : base(ValidationMode.None, subject, source, previousErrors) {
        Connector = new ValidationsConnector<string?, TextValidations>(Subject, this);
    }

    public IValidationsConnector<string?, TextValidations> IsNotEmptyOrWhiteSpace() {
        if (Subject is not null && Subject.Trim().Length == 0)
            Errors.Add(new(CannotBeEmptyOrWhitespace, Source));
        return Connector;
    }

    public IValidationsConnector<string?, TextValidations> MinimumLengthIs(int length) {
        if (Subject is not null && Subject.Length < length)
            Errors.Add(new(ErrorMessages.MinimumLengthIs, Source, length, Subject.Length));

        return Connector;
    }

    public IValidationsConnector<string?, TextValidations> IsIn(params string?[] list) {
        if (Subject is not null && !list.Contains(Subject))
            Errors.Add(new(MustBeIn, Source, string.Join(", ", list), Subject));

        return Connector;
    }

    public IValidationsConnector<string?, TextValidations> LengthIs(int length) {
        if (Subject is not null && Subject.Length != length)
            Errors.Add(new(LengthMustBe, Source, length, Subject.Length));

        return Connector;
    }

    public IValidationsConnector<string?, TextValidations> MaximumLengthIs(int length) {
        if (Subject is not null && Subject.Length > length)
            Errors.Add(new(ErrorMessages.MaximumLengthIs, Source, length, Subject.Length));

        return Connector;
    }

    public IValidationsConnector<string?, TextValidations> IsEmail() {
        if (Subject is not null && !EmailChecker().IsMatch(Subject))
            Errors.Add(new(IsNotAValidEmail, Source));

        return Connector;
    }

    public IValidationsConnector<string?, TextValidations> IsPassword(IPasswordPolicy policy) {
        if (string.IsNullOrWhiteSpace(Subject) || policy.TryValidate(Subject, out var errors))
            return Connector;
        Errors.Add(new(IsNotAValidPassword, Source));
        foreach(var error in errors) Errors.Add(error);
        return Connector;
    }

    [GeneratedRegex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex EmailChecker();
}