namespace RolePlayReady.Handlers.Auth;

public record User : IValidatable, IKey {
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public HashedSecret? HashedPassword { get; init; }
    public DateTime LockExpiration { get; init; } = DateTime.MinValue;
    public int SignInRetryCount { get; init; }
    public bool IsBlocked { get; init; }
    public ICollection<Role> Roles { get; init; } = Array.Empty<Role>();

    [PersonalInformation]
    public string? Name { get; init; }

    [PersonalInformation]
    public DateOnly? Birthday { get; init; }

    public string FolderName => (Base64Guid)Id;

    public ValidationResult Validate(IDictionary<string, object?>? context = null) {
        var result = ValidationResult.Success();
        result += Email.IsRequired()
            .And().IsNotEmptyOrWhiteSpace()
            .And().IsEmail().Result;
        return result;
    }
}