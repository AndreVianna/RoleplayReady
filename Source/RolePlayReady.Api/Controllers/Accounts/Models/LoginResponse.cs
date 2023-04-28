namespace RolePlayReady.Api.Controllers.Accounts.Models;

public record LoginResponse {
    public required string Token { get; set; }
}