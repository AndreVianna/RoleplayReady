using RolePlayReady.DataAccess.Repositories.GameSystems;

namespace RolePlayReady.DataAccess.Repositories.GameSystems;

public class GameSystemsRepository : IGameSystemsRepository {
    private readonly ITrackedJsonFileRepository _files;

    public GameSystemsRepository(ITrackedJsonFileRepository files) {
        _files = files;
    }

    public async Task<Result<IEnumerable<GameSystem>>> GetManyAsync(string owner, CancellationToken cancellation = default) {
        var files = await _files
            .GetAllAsync<GameSystemDataModel>(owner, string.Empty, cancellation)
            .ConfigureAwait(false);
        return files.Map(i => i.Map()!);
    }

    public async Task<Maybe<GameSystem>> GetByIdAsync(string owner, Guid id, CancellationToken cancellation = default) {
        var file = await _files
            .GetByIdAsync<GameSystemDataModel>(owner, string.Empty, id.ToString(), cancellation)
            .ConfigureAwait(false);
        return file.Map(i => i.Map());
    }

    public async Task<Result<GameSystem>> InsertAsync(string owner, GameSystem input, CancellationToken cancellation = default) {
        var result = await _files.UpsertAsync(owner, string.Empty, Guid.NewGuid().ToString(), input.Map(), cancellation).ConfigureAwait(false);
        return input with { Timestamp = result.Value };
    }

    public async Task<Result<GameSystem>> UpdateAsync(string owner, GameSystem input, CancellationToken cancellation = default) {
        var result = await _files.UpsertAsync(owner, string.Empty, input.Id.ToString(), input.Map(), cancellation);
        return input with { Timestamp = result.Value };
    }

    public Result<bool> Delete(string owner, Guid id)
        => _files.Delete(owner, string.Empty, id.ToString());
}
