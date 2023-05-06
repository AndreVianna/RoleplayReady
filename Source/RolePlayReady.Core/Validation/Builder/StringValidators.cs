namespace System.Validation.Builder;

public partial class StringValidators : Validators<string?>, IStringValidators {
    private readonly Connectors<string?, StringValidators> _connector;
    private readonly ValidationCommandFactory<string> _commandFactory;

    public static StringValidators CreateAsOptional(string? subject, string source)
        => new(subject, source);
    public static StringValidators CreateAsRequired(string? subject, string source)
        => new(subject, source, EnsureNotNull(subject, source));

    private StringValidators(string? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        _connector = new Connectors<string?, StringValidators>(this);
        _commandFactory = ValidationCommandFactory.For(Subject, Source, Result);
    }

    public IConnectors<string?, StringValidators> IsNotEmpty() {
        _commandFactory.Create(nameof(IsEmptyCommand)).Negate();
        return _connector;
    }

    public IConnectors<string?, StringValidators> IsNotEmptyOrWhiteSpace() {
        _commandFactory.Create(nameof(IsEmptyOrWhiteSpaceCommand)).Negate();
        return _connector;
    }

    public IConnectors<string?, StringValidators> MinimumLengthIs(int length) {
        _commandFactory.Create(nameof(MinimumLengthIsCommand), length).Validate();
        return _connector;
    }

    public IConnectors<string?, StringValidators> IsIn(params string[] list) {
        _commandFactory.Create(nameof(IsOneOfCommand<int>), list.OfType<object?>().ToArray()).Validate();
        return _connector;
    }

    public IConnectors<string?, StringValidators> LengthIs(int length) {
        _commandFactory.Create(nameof(LengthIsCommand), length).Validate();
        return _connector;
    }

    public IConnectors<string?, StringValidators> MaximumLengthIs(int length) {
        _commandFactory.Create(nameof(MaximumLengthIsCommand), length).Validate();
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