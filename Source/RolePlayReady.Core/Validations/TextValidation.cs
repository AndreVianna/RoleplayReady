namespace System.Validations;

public class TextValidation
    : Validation<string, ITextValidators>,
        ITextValidation {
    public TextValidation(string? subject, string? source, IEnumerable<ValidationError>? previousErrors = null)
    : base(subject, source, previousErrors) {
    }

    public IConnectsToOrFinishes<ITextValidators> IsNotEmptyOrWhiteSpace() {
        if (Subject is null)
            return this;
        if (Subject.Trim().Length == 0)
            Errors.Add(new(CannotBeEmptyOrWhitespace, Source));
        return this;
    }

    public IConnectsToOrFinishes<ITextValidators> MinimumLengthIs(int length) {
        if (Subject is null) return this;
        if (Subject.Length < length)
            Errors.Add(new(Constants.Constants.ErrorMessages.MinimumLengthIs, Source, length, Subject.Length));
        return this;
    }

    public IConnectsToOrFinishes<ITextValidators> IsIn(ICollection<string> list) {
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
        if (Subject is null)
            return this;
        if (Subject.Length > length)
            Errors.Add(new(Constants.Constants.ErrorMessages.MaximumLengthIs, Source, length, Subject.Length));
        return this;
    }
}
