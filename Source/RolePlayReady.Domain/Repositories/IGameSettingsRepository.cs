namespace RolePlayReady.Repositories;

public interface IGameSettingsRepository {
    Task<IEnumerable<GameSetting>> GetManyAsync(string owner, CancellationToken cancellation = default);
    Task<GameSetting?> GetByIdAsync(string owner, string id, CancellationToken cancellation = default);
    Task UpsertAsync(string owner, GameSetting gameSetting, CancellationToken cancellation = default);
    void Delete(string owner, string id);
}
