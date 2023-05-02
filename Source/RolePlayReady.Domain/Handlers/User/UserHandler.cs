namespace RolePlayReady.Handlers.User;

public class UserHandler
    : CrudHandler<User, UserRow, IUserRepository>,
      IUserHandler {
    public UserHandler(IUserRepository repository)
        : base(repository) {
    }
}