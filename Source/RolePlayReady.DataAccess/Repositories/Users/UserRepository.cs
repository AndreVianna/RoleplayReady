namespace RolePlayReady.DataAccess.Repositories.Users;

public class UserRepository : IUserRepository {
    private readonly IJsonFileStorage<UserData> _files;

    public UserRepository(IJsonFileStorage<UserData> files) {
        _files = files;
        files.SetBasePath("Users");
    }

    public async Task<IEnumerable<UserRow>> GetManyAsync(CancellationToken cancellation = default) {
        var files = await _files
            .GetAllAsync(cancellation)
            .ConfigureAwait(false);
        return files.ToArray(UserMapper.ToRow);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellation = default) {
        var file = await _files
            .GetByIdAsync(id, cancellation)
            .ConfigureAwait(false);
        return file.ToModel();
    }

    public async Task<User?> AddAsync(User input, CancellationToken cancellation = default) {
        var file = await _files.CreateAsync(input.ToData(), cancellation).ConfigureAwait(false);
        return file.ToModel();
    }

    public async Task<User?> UpdateAsync(User input, CancellationToken cancellation = default) {
        var file = await _files.UpdateAsync(input.ToData(), cancellation);
        return file.ToModel();
    }

    public bool Remove(Guid id)
        => _files.Delete(id);
}
