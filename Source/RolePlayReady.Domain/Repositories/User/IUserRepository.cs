using UserModel = RolePlayReady.Handlers.User.User;

namespace RolePlayReady.Repositories.User;

public interface IUserRepository : IRepository<UserModel, UserRow> {
}
