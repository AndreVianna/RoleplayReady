namespace RolePlayReady.DataAccess.Contracts;

public interface IDataFileRepository {
    Task<IEnumerable<DataFile<TData>>> GetAllAsync<TData>(string? path, CancellationToken cancellation = default);
    Task<DataFile<TData>?> GetByIdAsync<TData>(string? path, string id, CancellationToken cancellation = default);
    Task<bool> UpsertAsync<TData>(string? path, string id, TData data, CancellationToken cancellation = default);
    bool Delete(string? path, string id);
}
