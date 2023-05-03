namespace RolePlayReady.Handlers.Auth;

public record NewUser : IValidatable {
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? Name { get; init; }

    public ValidationResult Validate() {
        var result = ValidationResult.Success();
        result += Email.IsNotNull()
            .And.IsNotEmptyOrWhiteSpace()
            .And.IsEmail().Result;
        result += Password.IsNotNull()
            .And.IsNotEmptyOrWhiteSpace()
            .And.IsPassword(null).Result;
        return result;
    }
}