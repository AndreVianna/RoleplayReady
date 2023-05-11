using RolePlayReady.Handlers.Auth;

namespace RolePlayReady.DataAccess.Repositories.Users;

public class UserRepository : IUserRepository {
    private readonly IJsonFileStorage<UserData> _files;
    private readonly IHasher _hasher;

    public UserRepository(IJsonFileStorage<UserData> files, IHasher hasher) {
        _files = files;
        _hasher = hasher;
        files.SetBasePath("Users");
    }

    public async Task<IEnumerable<UserRow>> GetManyAsync(CancellationToken cancellation = default) {
        var files = await _files
            .GetAllAsync(cancellation)
            .ConfigureAwait(false);
        return files.ToArray(UserMapper.ToRow);
    }

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellation = default)
        => Task.FromResult<User?>(default);

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

    public async Task<bool> VerifyAsync(SignIn signIn, CancellationToken cancellation) {
        var file = await GetByEmailAsync(signIn.Email, cancellation).ConfigureAwait(false);
        return file?.HashedPassword is not null
               && _hasher.VerifySecret(signIn.Password, file.HashedPassword);
    }
}
