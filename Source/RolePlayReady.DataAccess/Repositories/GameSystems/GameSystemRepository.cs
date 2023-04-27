namespace RolePlayReady.DataAccess.Repositories.GameSystems;

public class GameSystemRepository : IGameSystemRepository {
    private readonly ITrackedJsonFileRepository<GameSystemData> _files;

    public GameSystemRepository(ITrackedJsonFileRepository<GameSystemData> files) {
        _files = files;
    }

    public async Task<IEnumerable<Row>> GetManyAsync(CancellationToken cancellation = default) {
        var files = await _files
            .GetAllAsync(string.Empty, cancellation)
            .ConfigureAwait(false);
        return files.ToArray(i => i.MapToRow());
    }

    public async Task<GameSystem?> GetByIdAsync(Guid id, CancellationToken cancellation = default) {
        var file = await _files
            .GetByIdAsync(string.Empty, id, cancellation)
            .ConfigureAwait(false);
        return file?.Map();
    }

    public async Task<GameSystem> InsertAsync(GameSystem input, CancellationToken cancellation = default) {
        var result = await _files.InsertAsync(string.Empty, input.Map(), cancellation).ConfigureAwait(false);
        return result.Map();
    }

    public async Task<GameSystem?> UpdateAsync(GameSystem input, CancellationToken cancellation = default) {
        var result = await _files.UpdateAsync(string.Empty, input.Map(), cancellation);
        return result?.Map();
    }

    public bool Delete(Guid id)
        => _files.Delete(string.Empty, id);
}
