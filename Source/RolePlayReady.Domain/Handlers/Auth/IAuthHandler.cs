namespace RolePlayReady.Handlers.Auth;

public interface IAuthHandler {
    SignInResult SignIn(Login login);
    SignInResult Register(User.User login);
}