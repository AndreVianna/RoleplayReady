namespace System.Validation.Builder;

public partial class TextValidators
    : Validators<string?, TextValidators>
        , ITextValidators {

    public static TextValidators CreateAsOptional(string? subject, string source)
        => new(subject, source);
    public static TextValidators CreateAsRequired(string? subject, string source)
        => new(subject, source, EnsureNotNull(subject, source));

    private TextValidators(string? subject, string source, ValidationResult? previousResult = null)
        : base(ValidatorMode.None, subject, source, previousResult) {
        Connector = new Connectors<string?, TextValidators>(Subject, this);
    }

    public IConnectors<string?, TextValidators> IsNotEmptyOrWhiteSpace() {
        CommandFactory.Create(nameof(IsNotEmptyOrWhiteSpace)).Validate();
        return Connector;
    }

    public IConnectors<string?, TextValidators> MinimumLengthIs(int length) {
        CommandFactory.Create(nameof(MinimumLengthIs), length).Validate();
        return Connector;
    }

    public IConnectors<string?, TextValidators> IsIn(params string?[] list) {
        CommandFactory.Create(nameof(IsIn), list).Validate();
        return Connector;
    }

    public IConnectors<string?, TextValidators> LengthIs(int length) {
        CommandFactory.Create(nameof(LengthIs), length).Validate();
        return Connector;
    }

    public IConnectors<string?, TextValidators> MaximumLengthIs(int length) {
        CommandFactory.Create(nameof(MaximumLengthIs), length).Validate();
        return Connector;
    }

    public IConnectors<string?, TextValidators> IsEmail() {
        if (Subject is not null && !EmailChecker().IsMatch(Subject))
            Result.Errors.Add(new(MustBeAValidEmail, Source));

        return Connector;
    }

    public IConnectors<string?, TextValidators> IsPassword(IPasswordPolicy policy) {
        if (string.IsNullOrWhiteSpace(Subject) || policy.TryValidate(Subject, out var errors))
            return Connector;
        Result.Errors.Add(new(MustBeAValidPassword, Source));
        foreach (var error in errors)
            Result.Errors.Add(error);
        return Connector;
    }

    [GeneratedRegex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", RegexOptions.Compiled | RegexOptions.CultureInvariant)]
    private static partial Regex EmailChecker();
}