namespace RolePlayReady.Api.Models.AccountManagement;

public record LoginResponse {
    public required string Token { get; set; }
}