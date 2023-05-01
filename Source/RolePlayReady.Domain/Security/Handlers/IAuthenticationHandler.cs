namespace RolePlayReady.Security.Handlers;

public interface IAuthenticationHandler {
    SignInResult Authenticate(Login login);
}