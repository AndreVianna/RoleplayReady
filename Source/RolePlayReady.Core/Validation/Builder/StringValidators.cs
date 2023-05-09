namespace System.Validation.Builder;

public partial class StringValidators : Validators<string?>, IStringValidators {
    private readonly Connectors<string?, StringValidators> _connector;
    private readonly ValidationCommandFactory _commandFactory;

    public StringValidators(string? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        _connector = new Connectors<string?, StringValidators>(this);
        _commandFactory = ValidationCommandFactory.For(typeof(string), Source, Result);
    }

    public IConnectors<string?, StringValidators> IsEmpty() {
        _commandFactory.Create(nameof(IsEmpty)).Validate(Subject);
        return _connector;
    }

    public IConnectors<string?, StringValidators> IsNotEmpty() {
        _commandFactory.Create(nameof(IsEmpty)).Negate(Subject);
        return _connector;
    }

    public IConnectors<string?, StringValidators> IsEmptyOrWhiteSpace() {
        _commandFactory.Create(nameof(IsEmptyOrWhiteSpace)).Validate(Subject);
        return _connector;
    }

    public IConnectors<string?, StringValidators> IsNotEmptyOrWhiteSpace() {
        _commandFactory.Create(nameof(IsEmptyOrWhiteSpace)).Negate(Subject);
        return _connector;
    }

    public IConnectors<string?, StringValidators> LengthIsAtLeast(int length) {
        _commandFactory.Create(nameof(LengthIsAtLeast), length).Validate(Subject);
        return _connector;
    }

    public IConnectors<string?, StringValidators> Contains(string substring) {
        _commandFactory.Create(nameof(Contains), substring).Validate(Subject);
        return _connector;
    }

    public IConnectors<string?, StringValidators> IsIn(params string[] list) {
        _commandFactory.Create(nameof(IsIn), list.OfType<object?>().ToArray()).Validate(Subject);
        return _connector;
    }

    public IConnectors<string?, StringValidators> LengthIs(int length) {
        _commandFactory.Create(nameof(LengthIs), length).Validate(Subject);
        return _connector;
    }

    public IConnectors<string?, StringValidators> LengthIsAtMost(int length) {
        _commandFactory.Create(nameof(LengthIsAtMost), length).Validate(Subject);
        return _connector;
    }

    public IConnectors<string?, StringValidators> IsEmail() {
        if (Subject is not null && !EmailChecker().IsMatch(Subject))
            Result.Errors.Add(new(MustBeAValidEmail, Source));

        return _connector;
    }

    public IConnectors<string?, StringValidators> IsPassword(IPasswordPolicy policy) {
        if (string.IsNullOrWhiteSpace(Subject) || policy.TryValidate(Subject, out var errors))
            return _connector;
        Result.Errors.Add(new(MustBeAValidPassword, Source));
        foreach (var error in errors)
            Result.Errors.Add(error);
        return _connector;
    }

    [GeneratedRegex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex EmailChecker();
}