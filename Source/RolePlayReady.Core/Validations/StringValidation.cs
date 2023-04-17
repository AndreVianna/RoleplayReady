namespace System.Validations;

public class StringValidation
    : Validation<string, IStringValidation>,
        IStringValidation {

    public StringValidation(string? subject, string? source, IEnumerable<ValidationError>? previousErrors = null)
    : base(subject, source, previousErrors) {
    }

    public IConnectsToOrFinishes<IStringValidation> NotEmptyOrWhiteSpace() {
        if (Subject is null) return this;
        if (Subject.Trim().Length == 0) Errors.Add(new(CannotBeEmptyOrWhitespace, Source));
        return this;
    }

    public IConnectsToOrFinishes<IStringValidation> NotShorterThan(int minimumLength) {
        if (Subject is null) return this;
        if (Subject.Length < minimumLength) Errors.Add(new(CannotBeShorterThan, Source, minimumLength, Subject.Length));
        return this;
    }

    public IConnectsToOrFinishes<IStringValidation> NotLongerThan(int maximumLength) {
        if (Subject is null) return this;
        if (Subject.Length > maximumLength) Errors.Add(new(CannotBeLongerThan, Source, maximumLength, Subject.Length));
        return this;
    }
}
