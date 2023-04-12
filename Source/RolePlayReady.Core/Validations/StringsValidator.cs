namespace System.Validations;

public class StringsValidator :
    Validator<IList<string>, IStringsChecks, IStringsConnectors>,
    IStringsChecks,
    IStringsConnectors {

    public StringsValidator(IList<string> subject, string? source)
            : base(subject, source) {
    }

    public IStringsConnectors ItemsAre(Func<StringValidator, IStringConnectors> validate) {
        if (Subject is null)
            return this;
        for (var index = 0; index < Subject.Count; index++) {
            var validation = new StringValidator(Subject[index], $"{Source}[{index}]");
            Errors.AddRange(validate(validation).Result.Errors);
        }

        return this;
    }
}
