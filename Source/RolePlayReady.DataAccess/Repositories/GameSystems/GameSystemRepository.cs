namespace RolePlayReady.DataAccess.Repositories.GameSystems;

public class GameSystemRepository : IGameSystemRepository {
    private readonly IJsonFileStorage<GameSystemData> _files;

    public GameSystemRepository(IJsonFileStorage<GameSystemData> files, IUserAccessor owner) {
        _files = files;
        files.SetBasePath($"{owner.BaseFolder}/GameSystems");
    }


    public async Task<IEnumerable<Row>> GetManyAsync(CancellationToken cancellation = default) {
        var files = await _files
            .GetAllAsync(cancellation)
            .ConfigureAwait(false);
        return files.ToArray(GameSystemMapper.ToRow);
    }

    public async Task<GameSystem?> GetByIdAsync(Guid id, CancellationToken cancellation = default) {
        var file = await _files
            .GetByIdAsync(id, cancellation)
            .ConfigureAwait(false);
        return GameSystemMapper.ToModel(file);
    }

    public async Task<GameSystem?> AddAsync(GameSystem input, CancellationToken cancellation = default) {
        var file = await _files.CreateAsync(GameSystemMapper.ToData(input), cancellation).ConfigureAwait(false);
        return GameSystemMapper.ToModel(file);
    }

    public async Task<GameSystem?> UpdateAsync(GameSystem input, CancellationToken cancellation = default) {
        var file = await _files.UpdateAsync(GameSystemMapper.ToData(input), cancellation);
        return GameSystemMapper.ToModel(file);
    }

    public bool Remove(Guid id)
        => _files.Delete(id);
}
