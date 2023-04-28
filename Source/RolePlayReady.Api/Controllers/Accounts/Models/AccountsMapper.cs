namespace RolePlayReady.Api.Controllers.Accounts.Models;

internal static class AccountsMapper {
    public static Login ToDomain(this LoginRequest request)
        => new() {
            Email = request.Email.Trim(),
            Password = request.Password.Trim(),
        };

    public static LoginResponse ToLoginResponse(this string response)
        => new() { Token = response };
}
