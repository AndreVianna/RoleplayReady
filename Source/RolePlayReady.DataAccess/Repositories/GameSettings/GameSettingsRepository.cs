using RolePlayReady.Repositories;

namespace RolePlayReady.DataAccess.Repositories.GameSettings;

public class GameSettingsRepository : IGameSettingsRepository {
    private readonly IDataFileRepository _files;

    public GameSettingsRepository(IDataFileRepository files) {
        _files = files;
    }

    public async Task<IEnumerable<GameSetting>> GetManyAsync(string owner, CancellationToken cancellation = default) {
        var files = await _files
            .GetAllAsync<SettingDataModel>(owner, string.Empty, cancellation)
            .ConfigureAwait(false);
        return files.Select(GameSettingMapper.MapFrom).ToArray()!;
    }

    public async Task<GameSetting?> GetByIdAsync(string owner, string id, CancellationToken cancellation = default) {
        var file = await _files
            .GetByIdAsync<SettingDataModel>(owner, string.Empty, id, cancellation)
            .ConfigureAwait(false);
        return GameSettingMapper.MapFrom(file);
    }

    public Task UpsertAsync(string owner, GameSetting gameSetting, CancellationToken cancellation = default)
        => _files.UpsertAsync(owner, string.Empty, gameSetting.DataFileName, GameSettingMapper.MapFrom(gameSetting), cancellation);

    public void Delete(string owner, string id)
        => _files.Delete(owner, string.Empty, id);
}
