using UserModel = RolePlayReady.Handlers.Auth.User;

namespace RolePlayReady.Repositories.User;

public interface IUserRepository : IRepository<UserModel, UserRow> {
    Task<UserModel?> VerifyAsync(SignIn signIn, CancellationToken cancellation = default);
}
