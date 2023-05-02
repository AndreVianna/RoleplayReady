namespace RolePlayReady.Handlers.User;

public record UserRow : Row {
    public required string Email { get; init; }
}