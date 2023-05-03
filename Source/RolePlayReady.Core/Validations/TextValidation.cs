namespace System.Validations;

public partial class TextValidation
    : Validation<string, ITextValidators>,
        ITextValidation {
    public TextValidation(string? subject, string? source, IEnumerable<ValidationError>? previousErrors = null)
    : base(subject, source, previousErrors) {
    }

    public IConnectsToOrFinishes<ITextValidators> IsNotEmptyOrWhiteSpace() {
        if (Subject is null) return this;
        if (Subject.Trim().Length == 0)
            Errors.Add(new(CannotBeEmptyOrWhitespace, Source));
        return this;
    }

    public IConnectsToOrFinishes<ITextValidators> MinimumLengthIs(int length) {
        if (Subject is null) return this;
        if (Subject.Length < length)
            Errors.Add(new(ErrorMessages.MinimumLengthIs, Source, length, Subject.Length));
        return this;
    }

    public IConnectsToOrFinishes<ITextValidators> IsIn(params string?[] list) {
        if (Subject is null) return this;
        if (!list.Contains(Subject))
            Errors.Add(new(MustBeIn, Source, string.Join(", ", list), Subject));
        return this;
    }

    public IConnectsToOrFinishes<ITextValidators> LengthIs(int length) {
        if (Subject is null) return this;
        if (Subject.Length != length)
            Errors.Add(new(LengthMustBe, Source, length, Subject.Length));
        return this;
    }

    public IConnectsToOrFinishes<ITextValidators> MaximumLengthIs(int length) {
        if (Subject is null) return this;
        if (Subject.Length > length)
            Errors.Add(new(ErrorMessages.MaximumLengthIs, Source, length, Subject.Length));
        return this;
    }

    public IConnectsToOrFinishes<ITextValidators> IsEmail() {
        if (string.IsNullOrEmpty(Subject)) return this;
        if (!EmailChecker().IsMatch(Subject))
            Errors.Add(new(IsNotAValidEmail, Source));
        return this;
    }

    [GeneratedRegex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex EmailChecker();
}
