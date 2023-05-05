using System.Validation.Abstractions;

namespace RolePlayReady.Handlers.Auth;

public record NewUser : IValidatable {
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? Name { get; init; }

    public ValidationResult ValidateSelf() {
        var result = ValidationResult.Success();
        result += Email.IsRequired()
            .And().IsNotEmptyOrWhiteSpace()
            .And().IsEmail().Result;
        result += Password.IsRequired()
            .And().IsNotEmptyOrWhiteSpace()
            .And().IsPassword(null).Result;
        return result;
    }
}