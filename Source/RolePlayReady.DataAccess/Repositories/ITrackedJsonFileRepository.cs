namespace RolePlayReady.DataAccess.Repositories;

public interface ITrackedJsonFileRepository {
    Task<Object<IEnumerable<IDataFile<TData>>>> GetAllAsync<TData>(string owner, string path, CancellationToken cancellation = default);
    Task<Maybe<IDataFile<TData>>> GetByIdAsync<TData>(string owner, string path, string id, CancellationToken cancellation = default);
    Task<Object<DateTime>> UpsertAsync<TData>(string owner, string path, string id, TData data, CancellationToken cancellation = default);
    Object<bool> Delete(string owner, string path, string id);
}
