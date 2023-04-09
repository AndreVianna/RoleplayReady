namespace RolePlayReady.DataAccess.Contracts;

public interface ISettingRepository {
    Task<IEnumerable<Setting>> GetManyAsync(CancellationToken cancellation = default);
    Task<Setting?> GetByIdAsync(string id, CancellationToken cancellation = default);
    Task UpsertAsync(Setting setting, CancellationToken cancellation = default);
    void Delete(string id);
}
