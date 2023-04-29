namespace RolePlayReady.Security.Handlers;

public interface IAuthenticationHandler {
    public const string AuthenticationFailedError = "AuthenticationFailed";

    Result<string> Authenticate(Login login);

    string AuthenticationFailedCode { get; }
}