namespace System.Validations;

public class StringValidation
    : Validation<string, IStringValidators>,
        IStringValidation {

    public StringValidation(string? subject, string? source, IEnumerable<ValidationError>? previousErrors = null)
    : base(subject, source, previousErrors) {
    }

    public IConnectsToOrFinishes<IStringValidators> IsNotEmptyOrWhiteSpace() {
        if (Subject is null) return this;
        if (Subject.Trim().Length == 0) Errors.Add(new(CannotBeEmptyOrWhitespace, Source));
        return this;
    }

    public IConnectsToOrFinishes<IStringValidators> MinimumLengthIs(int length) {
        if (Subject is null) return this;
        if (Subject.Length < length) Errors.Add(new(Constants.Constants.ErrorMessages.MinimumLengthIs, Source, length, Subject.Length));
        return this;
    }

    public IConnectsToOrFinishes<IStringValidators> LengthIs(int length) {
        if (Subject is null) return this;
        if (Subject.Length != length)
            Errors.Add(new(LengthMustBe, Source, length, Subject.Length));
        return this;
    }

    public IConnectsToOrFinishes<IStringValidators> MaximumLengthIs(int length) {
        if (Subject is null)
            return this;
        if (Subject.Length > length)
            Errors.Add(new(Constants.Constants.ErrorMessages.MaximumLengthIs, Source, length, Subject.Length));
        return this;
    }
}
