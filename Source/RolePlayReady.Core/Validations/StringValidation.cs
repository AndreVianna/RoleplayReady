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

    public IConnectsToOrFinishes<IStringValidation> NoShorterThan(int length) {
        if (Subject is null) return this;
        if (Subject.Length < length) Errors.Add(new(MinimumLengthIs, Source, length, Subject.Length));
        return this;
    }

    public IConnectsToOrFinishes<IStringValidation> NoLongerThan(int length) {
        if (Subject is null) return this;
        if (Subject.Length > length) Errors.Add(new(MaximumLengthIs, Source, length, Subject.Length));
        return this;
    }

    public IConnectsToOrFinishes<IStringValidation> Exactly(int length) {
        if (Subject is null) return this;
        if (Subject.Length != length)
            Errors.Add(new(LengthMustBe, Source, length, Subject.Length));
        return this;
    }
}
