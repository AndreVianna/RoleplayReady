namespace RolePlayReady.Repositories;

public interface IGameSettingsRepository {
    Task<IEnumerable<GameSystemSetting>> GetManyAsync(string owner, CancellationToken cancellation = default);
    Task<GameSystemSetting?> GetByIdAsync(string owner, string id, CancellationToken cancellation = default);
    Task UpsertAsync(string owner, GameSystemSetting gameSystemSetting, CancellationToken cancellation = default);
    void Delete(string owner, string id);
}
