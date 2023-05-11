namespace RolePlayReady.Api.Controllers.Auth.Models;

internal static class AuthMapper {
    public static SignIn ToDomain(this LoginRequest request)
        => new() {
            Email = request.Email.Trim(),
            Password = request.Password.Trim(),
        };

    public static LoginResponse ToLoginResponse(this string response)
        => new() { Token = response };
}
