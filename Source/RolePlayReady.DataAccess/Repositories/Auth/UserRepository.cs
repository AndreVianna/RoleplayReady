namespace RolePlayReady.DataAccess.Repositories.Auth;

public class UserRepository : IUserRepository {
    private static IDictionary<string, Guid>? _emailIndex;

    private readonly IJsonFileStorage<UserData> _users;
    private readonly IHasher _hasher;

    public UserRepository(IJsonFileStorage<UserData> users, IHasher hasher) {
        _users = users;
        _hasher = hasher;
        users.SetBasePath("Users");
        _emailIndex ??= LoadEmailIndex();
    }

    private IDictionary<string, Guid> LoadEmailIndex() {
        var users = _users.GetAllAsync().Result;
        return users.ToDictionary(i => i.Email, i => i.Id);
    }

    public async Task<IEnumerable<UserRow>> GetManyAsync(CancellationToken cancellation = default) {
        var users = await _users
                        .GetAllAsync(cancellation: cancellation)
                        .ConfigureAwait(false);
        return users.ToArray(UserMapper.ToRow);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellation = default) {
        var userData = await _users
                            .GetByIdAsync(id, cancellation)
                            .ConfigureAwait(false);
        return userData.ToModel();
    }

    public async Task<User?> AddAsync(User input, CancellationToken cancellation = default) {
        if (_emailIndex!.ContainsKey(input.Email)) return default; 
        var userData = await _users.CreateAsync(input.ToData(), cancellation).ConfigureAwait(false);
        if (userData is not null) _emailIndex![userData.Email] = userData.Id;
        return userData.ToModel();
    }

    public async Task<User?> UpdateAsync(User input, CancellationToken cancellation = default) {
        var userData = await _users.UpdateAsync(input.ToData(), cancellation);
        return userData.ToModel();
    }

    public bool Remove(Guid id) {
        var isDeleted = _users.Delete(id);
        if (isDeleted) _emailIndex!.Remove(_emailIndex.First(i => i.Value == id).Key);
        return isDeleted;
    }

    public async Task<User?> VerifyAsync(SignIn signIn, CancellationToken cancellation) {
        var user = await GetByEmailAsync(signIn.Email, cancellation).ConfigureAwait(false);
        return user?.HashedPassword?.Verify(signIn.Password, _hasher) ?? false
            ? user
            : default;
    }

    private async Task<User?> GetByEmailAsync(string email, CancellationToken cancellation = default)
        => _emailIndex!.ContainsKey(email)
            ? await GetByIdAsync(_emailIndex[email], cancellation)
            : default;
}
