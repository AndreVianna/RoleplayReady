namespace RolePlayReady.Security.Models;

public record User : IKey, IValidatable {
    public required Guid Id { get; init; }
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string PasswordHash { get; init; }
    public required string PasswordSalt { get; init; }
    public DateTime LockExpiration { get; init; } = DateTime.MinValue;
    public int SignInRetryCount { get; init; }
    public bool IsBlocked { get; init; }
    public ICollection<Role> Roles { get; init; } = Array.Empty<Role>();

    [PersonalInformation]
    public string? Name { get; init; }
    [PersonalInformation]
    public DateOnly? Birthday { get; init; }

    public Result Validate() {
        var result = Result.AsSuccess();
        result += Username.IsNotNull()
                          .And.IsNotEmptyOrWhiteSpace()
                          .And.MinimumLengthIs(3)
                          .And.MaximumLengthIs(20).Result;
        result += Email.IsNotNull()
                       .And.IsNotEmptyOrWhiteSpace()
                       .And.IsEmail().Result;
        result += Name.IsNullOr()
                      .IsNotEmptyOrWhiteSpace()
                      .And.MinimumLengthIs(2)
                      .And.MaximumLengthIs(100).Result;
        return result;
    }
}

internal class PersonalInformationAttribute : Attribute {
}

public enum Role {
    User = 1,
    Administrator = 2,
}