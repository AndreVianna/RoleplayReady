namespace RolePlayReady.DataAccess.Repositories.Users;

public class UserRepository : Repository<User, UserRow, UserData>, IUserRepository {
    public UserRepository(IJsonFileHandler<UserData> files, IUserMapper mapper)
        : base(files, mapper) {
        files.SetBasePath("Users");
    }
}
