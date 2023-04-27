namespace RolePlayReady.Api.Models.AccountManagement;

public record LoginRequest {
    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
}