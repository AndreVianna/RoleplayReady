namespace RolePlayReady.Handlers.Auth;

public record UserRow : Row {
    public required string Email { get; init; }
}