namespace System.Validations;

public class StringValidation
    : Validation<string, IStringValidation>,
        IStringValidation {

    public StringValidation(string? subject, string? source, ICollection<ValidationError>? previousErrors = null)
    : base(subject, source, previousErrors) {
    }

    public IStringValidation NotNull() {
        if (Subject is null) Errors.Add(new(CannotBeNull, Source));
        return this;
    }

    public IStringValidation NotEmptyOrWhiteSpace() {
        if (Subject is null) return this;
        if (Subject.Trim().Length == 0) Errors.Add(new(CannotBeEmptyOrWhitespace, Source));
        return this;
    }

    public IStringValidation NotShorterThan(int minimumLength) {
        var length = Subject?.Length ?? 0;
        if (length < minimumLength) Errors.Add(new(CannotBeShorterThan, Source, minimumLength, length));
        return this;
    }

    public IStringValidation NotLongerThan(int maximumLength) {
        var length = Subject?.Length ?? 0;
        if (length > maximumLength) Errors.Add(new(CannotBeLongerThan, Source, maximumLength, length));
        return this;
    }
}
