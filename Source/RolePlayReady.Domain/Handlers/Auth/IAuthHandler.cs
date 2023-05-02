namespace RolePlayReady.Handlers.Auth;

public interface IAuthHandler {
    SignInResult Authenticate(Login login);
}