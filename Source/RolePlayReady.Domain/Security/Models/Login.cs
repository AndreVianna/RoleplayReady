namespace RolePlayReady.Security.Models;

public record Login : IValidatable {
    public required string Email { get; set; }
    public required string Password { get; set; }

    public Result Validate() {
        var result = Result.AsSuccess();
        result += Email.IsNotNull()
                       .And.IsNotEmptyOrWhiteSpace()
                       .And.IsEmail()
                       .Result;
        result += Password.IsNotNull()
                          .And.IsNotEmptyOrWhiteSpace()
                          .And.MaximumLengthIs(Constants.Validation.Password.MaximumLength)
                          .Result;
        return result;
    }
}
