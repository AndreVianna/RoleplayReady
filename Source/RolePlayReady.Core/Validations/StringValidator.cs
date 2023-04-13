namespace System.Validations;

public class StringValidator
    : Validator<string, IStringChecks, IStringConnectors>,
        IStringValidator {

    public StringValidator(string? subject, string? source)
        : base(subject, source) {
    }

    public IStringConnectors NotEmptyOrWhiteSpace() {
        if (Subject is null)
            return this;
        if (Subject.Trim().Length == 0)
            Errors.Add(new(string.Format(EmptyOrWhitespace, Source), Source));
        return this;
    }

    public IStringConnectors NoLongerThan(int maximumLength) {
        if ((Subject?.Length ?? 0) > maximumLength)
            Errors.Add(new(string.Format(LongerThan, Source, maximumLength), Source));
        return this;
    }
}