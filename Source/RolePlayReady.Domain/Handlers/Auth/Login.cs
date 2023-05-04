namespace RolePlayReady.Handlers.Auth;

public record Login : IValidatable {
    public required string Email { get; set; }
    public required string Password { get; set; }

    public ICollection<ValidationError> Validate() {
        var result = ValidationResult.Success();
        result += Email.IsNotNull()
                       .And.IsNotEmptyOrWhiteSpace()
                       .And.IsEmail().Errors;
        result += Password.IsNotNull()
                          .And.IsNotEmptyOrWhiteSpace()
                          .And.MaximumLengthIs(Validation.Password.MaximumLength).Errors;
        return result.Errors;
    }
}