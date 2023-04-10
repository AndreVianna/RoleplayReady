namespace RolePlayReady.DataAccess.Repositories;

public interface IDataFileRepository
{
    Task<IEnumerable<DataFile<TData>>> GetAllAsync<TData>(string owner, string path, CancellationToken cancellation = default);
    Task<DataFile<TData>?> GetByIdAsync<TData>(string owner, string path, string id, CancellationToken cancellation = default);
    Task<bool> UpsertAsync<TData>(string owner, string path, string id, TData data, CancellationToken cancellation = default);
    bool Delete(string owner, string path, string id);
}
