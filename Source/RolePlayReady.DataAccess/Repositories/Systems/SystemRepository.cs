namespace RolePlayReady.DataAccess.Repositories.Systems;

public class SystemRepository : ISystemRepository {
    private readonly IJsonFileStorage<SystemData> _files;

    public SystemRepository(IJsonFileStorage<SystemData> files, IUserAccessor owner) {
        _files = files;
        files.SetBasePath($"{owner.BaseFolder}/Systems");
    }

    public async Task<IEnumerable<Row>> GetManyAsync(CancellationToken cancellation = default) {
        var files = await _files
            .GetAllAsync(cancellation)
            .ConfigureAwait(false);
        return files.ToArray(SystemMapper.ToRow);
    }

    public async Task<Handlers.System.System?> GetByIdAsync(Guid id, CancellationToken cancellation = default) {
        var file = await _files
            .GetByIdAsync(id, cancellation)
            .ConfigureAwait(false);
        return SystemMapper.ToModel(file);
    }

    public async Task<Handlers.System.System?> AddAsync(Handlers.System.System input, CancellationToken cancellation = default) {
        var file = await _files.CreateAsync(SystemMapper.ToData(input), cancellation).ConfigureAwait(false);
        return SystemMapper.ToModel(file);
    }

    public async Task<Handlers.System.System?> UpdateAsync(Handlers.System.System input, CancellationToken cancellation = default) {
        var file = await _files.UpdateAsync(SystemMapper.ToData(input), cancellation);
        return SystemMapper.ToModel(file);
    }

    public bool Remove(Guid id)
        => _files.Delete(id);
}
