namespace RolePlayReady.DataAccess.Repositories.Systems;

public class SystemRepository : ISystemRepository {
    private readonly IJsonFileStorage<SystemData> _files;

    public SystemRepository(IJsonFileStorage<SystemData> files, IUserAccessor owner) {
        _files = files;
        files.SetBasePath($"{owner.BaseFolder}/Systems");
    }

    public async Task<IEnumerable<SystemRow>> GetManyAsync(CancellationToken ct = default) {
        var files = await _files
            .GetAllAsync(ct: ct)
            .ConfigureAwait(false);
        return files.ToArray(SystemMapper.ToRow);
    }

    public async Task<Handlers.System.System?> GetByIdAsync(Guid id, CancellationToken ct = default) {
        var file = await _files
            .GetByIdAsync(id, ct)
            .ConfigureAwait(false);
        return SystemMapper.ToModel(file);
    }

    public async Task<Handlers.System.System?> AddAsync(Handlers.System.System input, CancellationToken ct = default) {
        var file = await _files.CreateAsync(SystemMapper.ToData(input), ct).ConfigureAwait(false);
        return SystemMapper.ToModel(file);
    }

    public async Task<Handlers.System.System?> UpdateAsync(Handlers.System.System input, CancellationToken ct = default) {
        var file = await _files.UpdateAsync(SystemMapper.ToData(input), ct);
        return SystemMapper.ToModel(file);
    }

    public Task<bool> RemoveAsync(Guid id, CancellationToken ct = default)
        => Task.Run(() => _files.Delete(id), ct);
}
