namespace RolePlayReady.DataAccess.Repositories;

public interface ITrackedJsonFileRepository {
    Task<ObjectResult<IEnumerable<DataFile<TData>>>> GetAllAsync<TData>(string owner, string path, CancellationToken cancellation = default);
    Task<NullableResult<DataFile<TData>>> GetByIdAsync<TData>(string owner, string path, string id, CancellationToken cancellation = default);
    Task<ObjectResult<DateTime>> UpsertAsync<TData>(string owner, string path, string id, TData data, CancellationToken cancellation = default);
    ObjectResult<bool> Delete(string owner, string path, string id);
}
