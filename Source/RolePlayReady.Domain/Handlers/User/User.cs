using System.Validation;

namespace RolePlayReady.Handlers.User;

public record User : IKey, IValidatable {
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public DateTime LockExpiration { get; init; } = DateTime.MinValue;
    public int SignInRetryCount { get; init; }
    public bool IsBlocked { get; init; }
    public ICollection<Role> Roles { get; init; } = Array.Empty<Role>();

    [PersonalInformation]
    public string? Name { get; init; }
    [PersonalInformation]
    public DateOnly? Birthday { get; init; }

    public string FolderName => (Base64Guid)Id;

    public ValidationResult ValidateSelf(bool negate = false) {
        var result = ValidationResult.Success();
        result += Email.IsRequired()
                       .And().IsNotEmptyOrWhiteSpace()
                       .And().IsEmail().Result;
        return result;
    }
}