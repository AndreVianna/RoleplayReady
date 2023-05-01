namespace RolePlayReady.Security.Models;

public record UserRow : Row {
    public required string Email { get; init; }
}