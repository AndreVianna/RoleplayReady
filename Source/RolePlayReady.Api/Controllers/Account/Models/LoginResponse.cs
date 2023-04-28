namespace RolePlayReady.Api.Controllers.Account.Models;

public record LoginResponse {
    public required string Token { get; set; }
}