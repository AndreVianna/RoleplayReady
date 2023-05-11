namespace RolePlayReady.Handlers.Auth;

public abstract record Identity : IValidatable {
    public string? Name { get; init; }
    public required string Email { get; init; }
    public HashedSecret HashedPassword { get; init; } = null!;

    public ValidationResult Validate(IDictionary<string, object?>? context = null) {
        var result = ValidationResult.Success();
        result += Email.IsRequired()
            .And().IsNotEmptyOrWhiteSpace()
            .And().IsEmail().Result;
        result += HashedPassword.IsRequired().Result;
        return result;
    }
}