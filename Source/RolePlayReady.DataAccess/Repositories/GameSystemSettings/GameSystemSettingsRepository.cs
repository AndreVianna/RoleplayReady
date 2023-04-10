using System.Results;

using DataModel = RolePlayReady.DataAccess.Repositories.GameSystemSettings.GameSystemSettingDataModel;

namespace RolePlayReady.DataAccess.Repositories.GameSystemSettings;

public class GameSystemSettingsRepository : IGameSystemSettingsRepository {
    private readonly ITrackedJsonFileRepository _files;

    public GameSystemSettingsRepository(ITrackedJsonFileRepository files) {
        _files = files;
    }

    public async Task<Result<IEnumerable<GameSystemSetting>>> GetManyAsync(string owner, CancellationToken cancellation = default) {
        var files = await _files
            .GetAllAsync<DataModel>(owner, string.Empty, cancellation)
            .ConfigureAwait(false);
        return files.Map(i => i.Map());
    }

    public async Task<Maybe<GameSystemSetting>> GetByIdAsync(string owner, Guid id, CancellationToken cancellation = default) {
        var file = await _files
            .GetByIdAsync<DataModel>(owner, string.Empty, id.ToString(), cancellation)
            .ConfigureAwait(false);
        return file.Map(i => i.Map());
    }

    public async Task<Result<GameSystemSetting>> InsertAsync(string owner, GameSystemSetting input, CancellationToken cancellation = default) {
        var result = await _files.UpsertAsync(owner, string.Empty, Guid.NewGuid().ToString(), input.Map(), cancellation).ConfigureAwait(false);
        return input with { Timestamp = result.Value };
    }

    public async Task<Result<GameSystemSetting>> UpdateAsync(string owner, GameSystemSetting input, CancellationToken cancellation = default) {
        var result = await _files.UpsertAsync(owner, string.Empty, input.Id.ToString(), input.Map(), cancellation);
        return input with { Timestamp = result.Value };
    }

    public Result<bool> Delete(string owner, Guid id)
        => _files.Delete(owner, string.Empty, id.ToString());
}
