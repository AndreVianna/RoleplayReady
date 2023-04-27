namespace RolePlayReady.Security.Handlers;

public interface IAuthenticationHandler {
    Result<string> Authenticate(Login login);
}