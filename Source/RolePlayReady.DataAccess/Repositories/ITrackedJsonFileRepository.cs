namespace RolePlayReady.DataAccess.Repositories;

public interface ITrackedJsonFileRepository {
    Task<ResultOf<IEnumerable<IDataFile<TData>>>> GetAllAsync<TData>(string owner, string path, CancellationToken cancellation = default);
    Task<ResultOf<IDataFile<TData>>> GetByIdAsync<TData>(string owner, string path, string id, CancellationToken cancellation = default);
    Task<ResultOf<DateTime>> UpsertAsync<TData>(string owner, string path, string id, TData data, CancellationToken cancellation = default);
    ResultOf<bool> Delete(string owner, string path, string id);
}
