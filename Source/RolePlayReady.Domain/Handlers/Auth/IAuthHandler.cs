namespace RolePlayReady.Handlers.Auth;

public interface IAuthHandler : ICrudHandler<User, UserRow> {
    Task<SignInResult> SignInAsync(SignIn signIn, CancellationToken cancellation = default);
    Task<CrudResult> RegisterAsync(SignOn signOn, CancellationToken cancellation = default);
}