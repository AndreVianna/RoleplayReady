namespace RolePlayReady.DataAccess.Repositories;

public interface ITrackedJsonFileRepository<TData>
    where TData : class {
    Task<IEnumerable<TData>> GetAllAsync(string owner, string path, CancellationToken cancellation = default);
    Task<TData?> GetByIdAsync(string owner, string path, Guid id, CancellationToken cancellation = default);
    Task<TData> UpsertAsync(string owner, string path, TData data, CancellationToken cancellation = default);
    Result<bool> Delete(string owner, string path, Guid id);
}
