namespace RolePlayReady.Api.Controllers.Auth.Models;

public record RoleRequest {
    [Required]
    public required Role Role { get; set; }
}