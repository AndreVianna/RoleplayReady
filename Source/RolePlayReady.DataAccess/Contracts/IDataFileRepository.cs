namespace RolePlayReady.DataAccess.Contracts;

public interface IDataFileRepository {
    Task<IEnumerable<DataFile<TData>>> GetAllAsync<TData>(string path, CancellationToken cancellation = default);
    Task<IEnumerable<DataFile<TData>>> GetAllAsync<TData>(CancellationToken cancellation = default);
    Task<DataFile<TData>?> GetByIdAsync<TData>(string path, string id, CancellationToken cancellation = default);
    Task<DataFile<TData>?> GetByIdAsync<TData>(string id, CancellationToken cancellation = default);
    Task UpsertAsync<TData>(string path, string id, TData data, CancellationToken cancellation = default);
    Task UpsertAsync<TData>(string id, TData data, CancellationToken cancellation = default);
    void Delete(string path, string id);
    void Delete(string id);
}