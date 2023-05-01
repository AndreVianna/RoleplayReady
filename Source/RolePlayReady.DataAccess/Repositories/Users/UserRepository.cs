namespace RolePlayReady.DataAccess.Repositories.Users;

public class UserRepository : IUserRepository {
    private readonly ITrackedJsonFileRepository<UserData> _files;

    public UserRepository(ITrackedJsonFileRepository<UserData> files) {
        _files = files;
    }

    public async Task<IEnumerable<UserRow>> GetManyAsync(CancellationToken cancellation = default) {
        var files = await _files
            .GetAllAsync(string.Empty, cancellation)
            .ConfigureAwait(false);
        return files.ToArray(i => i.MapToRow());
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellation = default) {
        var file = await _files
            .GetByIdAsync(string.Empty, id, cancellation)
            .ConfigureAwait(false);
        return file?.Map();
    }

    public async Task<User> AddAsync(User input, CancellationToken cancellation = default) {
        var result = await _files.InsertAsync(string.Empty, input.Map(), cancellation).ConfigureAwait(false);
        return result.Map();
    }

    public async Task<User?> UpdateAsync(User input, CancellationToken cancellation = default) {
        var result = await _files.UpdateAsync(string.Empty, input.Map(), cancellation);
        return result?.Map();
    }

    public bool Remove(Guid id)
        => _files.Delete(string.Empty, id);
}
