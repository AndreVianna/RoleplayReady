namespace System.Validations;

public class StringValidation
    : Validation<string, StringValidation, IStringValidations>,
        IStringValidations {

    public StringValidation(string? subject, string? source, ICollection<ValidationError>? previousErrors = null)
    : base(subject, source, previousErrors) {
    }

    public IConnectors<IStringValidations> NotNull() {
        if (Subject is null)
            Errors.Add(new(CannotBeNull, Source));
        return this;
    }

    public IConnectors<IStringValidations> NotEmptyOrWhiteSpace() {
        if (Subject is null)
            return this;
        if (Subject.Trim().Length == 0)
            Errors.Add(new(CannotBeEmptyOrWhitespace, Source));
        return this;
    }

    public IConnectors<IStringValidations> MaximumLengthOf(int maximumLength) {
        var subjectLength = Subject?.Length ?? 0;
        if (subjectLength > maximumLength)
            Errors.Add(new(CannotBeLongerThan, Source, maximumLength, subjectLength));
        return this;
    }

    public IConnectors<IStringValidations> MinimumLengthOf(int minimumLength) {
        var subjectLength = Subject?.Length ?? 0;
        if (subjectLength < minimumLength)
            Errors.Add(new(CannotBeShorterThan, Source, minimumLength, subjectLength));
        return this;
    }
}
