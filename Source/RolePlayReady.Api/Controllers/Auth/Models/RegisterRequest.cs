namespace RolePlayReady.Api.Controllers.Auth.Models;

public record RegisterRequest
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public string? Name { get; set; }
}
