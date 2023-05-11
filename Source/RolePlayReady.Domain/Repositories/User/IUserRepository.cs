using UserModel = RolePlayReady.Handlers.Auth.User;

namespace RolePlayReady.Repositories.User;

public interface IUserRepository : IRepository<UserModel, UserRow> {
    Task<UserModel?> GetByEmailAsync(string email, CancellationToken cancellation = default);
    Task<bool> VerifyAsync(SignIn signIn, CancellationToken cancellation = default);
}
