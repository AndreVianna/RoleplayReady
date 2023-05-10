namespace System.Validation.Builder;

public partial class StringValidator : Validator<string?>, IStringValidator {
    private readonly ValidationCommandFactory _commandFactory;

    public StringValidator(string? subject, string source, ValidatorMode mode = ValidatorMode.And)
        : base(subject, source, mode) {
        Connector = new Connector<string?, StringValidator>(this);
        _commandFactory = ValidationCommandFactory.For(typeof(string), Source);
    }

    public IConnector<StringValidator> Connector { get; }

    public IConnector<StringValidator> IsNull() {
        Result += _commandFactory.Create(nameof(IsNull)).Validate(Subject);
        return Connector;
    }

    public IConnector<StringValidator> IsNotNull() {
        Result += _commandFactory.Create(nameof(IsNull)).Negate(Subject);
        return Connector;
    }

    public IConnector<StringValidator> IsEmpty() {
        Result += _commandFactory.Create(nameof(IsEmpty)).Validate(Subject);
        return Connector;
    }

    public IConnector<StringValidator> IsNotEmpty() {
        Result += _commandFactory.Create(nameof(IsEmpty)).Negate(Subject);
        return Connector;
    }

    public IConnector<StringValidator> IsEmptyOrWhiteSpace() {
        Result += _commandFactory.Create(nameof(IsEmptyOrWhiteSpace)).Validate(Subject);
        return Connector;
    }

    public IConnector<StringValidator> IsNotEmptyOrWhiteSpace() {
        Result += _commandFactory.Create(nameof(IsEmptyOrWhiteSpace)).Negate(Subject);
        return Connector;
    }

    public IConnector<StringValidator> LengthIsAtLeast(int length) {
        Result += _commandFactory.Create(nameof(LengthIsAtLeast), length).Validate(Subject);
        return Connector;
    }

    public IConnector<StringValidator> Contains(string substring) {
        Result += _commandFactory.Create(nameof(Contains), substring).Validate(Subject);
        return Connector;
    }

    public IConnector<StringValidator> IsIn(params string[] list) {
        Result += _commandFactory.Create(nameof(IsIn), list.OfType<object?>().ToArray()).Validate(Subject);
        return Connector;
    }

    public IConnector<StringValidator> LengthIs(int length) {
        Result += _commandFactory.Create(nameof(LengthIs), length).Validate(Subject);
        return Connector;
    }

    public IConnector<StringValidator> LengthIsAtMost(int length) {
        Result += _commandFactory.Create(nameof(LengthIsAtMost), length).Validate(Subject);
        return Connector;
    }

    public IConnector<StringValidator> IsEmail() {
        if (Subject is not null && !EmailChecker().IsMatch(Subject))
            Result += new ValidationError(MustBeAValidEmail, Source);

        return Connector;
    }

    public IConnector<StringValidator> IsPassword(IPasswordPolicy policy) {
        if (string.IsNullOrWhiteSpace(Subject) || policy.TryValidate(Subject, out var errors))
            return Connector;
        Result += new ValidationError(MustBeAValidPassword, Source);
        foreach (var error in errors)
            Result += error;
        return Connector;
    }

    [GeneratedRegex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex EmailChecker();
}