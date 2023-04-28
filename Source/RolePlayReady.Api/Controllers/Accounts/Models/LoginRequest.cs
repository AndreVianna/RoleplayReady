namespace RolePlayReady.Api.Controllers.Accounts.Models;

public record LoginRequest {
    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
}