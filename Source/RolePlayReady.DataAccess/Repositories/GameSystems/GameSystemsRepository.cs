namespace RolePlayReady.DataAccess.Repositories.GameSystems;

public class GameSystemsRepository : IGameSystemsRepository {
    private readonly ITrackedJsonFileRepository _files;

    public GameSystemsRepository(ITrackedJsonFileRepository files) {
        _files = files;
    }

    public async Task<ObjectResult<IEnumerable<GameSystem>>> GetManyAsync(string owner, CancellationToken cancellation = default) {
        var files = await _files
            .GetAllAsync<GameSystemDataModel>(owner, string.Empty, cancellation)
            .ConfigureAwait(false);
        return files.Map(i => i.Map()!);
    }

    public async Task<NullableResult<GameSystem>> GetByIdAsync(string owner, Guid id, CancellationToken cancellation = default) {
        var file = await _files
            .GetByIdAsync<GameSystemDataModel>(owner, string.Empty, id.ToString(), cancellation)
            .ConfigureAwait(false);
        return file.Map(i => i.Map());
    }

    public async Task<ObjectResult<GameSystem>> InsertAsync(string owner, GameSystem input, CancellationToken cancellation = default) {
        var result = await _files.UpsertAsync(owner, string.Empty, Guid.NewGuid().ToString(), input.Map(), cancellation).ConfigureAwait(false);
        return input with { Timestamp = result.Value };
    }

    public async Task<ObjectResult<GameSystem>> UpdateAsync(string owner, GameSystem input, CancellationToken cancellation = default) {
        var result = await _files.UpsertAsync(owner, string.Empty, input.Id.ToString(), input.Map(), cancellation);
        return input with { Timestamp = result.Value };
    }

    public ObjectResult<bool> Delete(string owner, Guid id)
        => _files.Delete(owner, string.Empty, id.ToString());
}
