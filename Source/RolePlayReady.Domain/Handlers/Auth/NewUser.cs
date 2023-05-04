namespace RolePlayReady.Handlers.Auth;

public record NewUser : IValidatable {
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? Name { get; init; }

    public ICollection<ValidationError> Validate() {
        var result = ValidationResult.Success();
        result += Email.IsNotNull()
            .And.IsNotEmptyOrWhiteSpace()
            .And.IsEmail().Errors;
        result += Password.IsNotNull()
            .And.IsNotEmptyOrWhiteSpace()
            .And.IsPassword(null).Errors;
        return result.Errors;
    }
}