using System.Validation;

namespace RolePlayReady.Handlers.Auth;

public record Login : IValidatable {
    public required string Email { get; set; }
    public required string Password { get; set; }

    public ValidationResult ValidateSelf(bool negate = false) {
        var result = ValidationResult.Success();
        result += Email.IsRequired()
                       .And().IsNotEmptyOrWhiteSpace()
                       .And().IsEmail().Result;
        result += Password.IsRequired()
                          .And().IsNotEmptyOrWhiteSpace()
                          .And().LengthIsAtMost(Validation.Password.MaximumLength).Result;
        return result;
    }
}