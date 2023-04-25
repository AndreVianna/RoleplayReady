namespace RolePlayReady.DataAccess.Repositories;

public interface ITrackedJsonFileRepository<TData>
    where TData : class, IKey {
    Task<IEnumerable<TData>> GetAllAsync(string owner, string path, CancellationToken cancellation = default);
    Task<TData?> GetByIdAsync(string owner, string path, Guid id, CancellationToken cancellation = default);
    Task<TData> InsertAsync(string owner, string path, TData data, CancellationToken cancellation = default);
    Task<TData?> UpdateAsync(string owner, string path, TData data, CancellationToken cancellation = default);
    Result Delete(string owner, string path, Guid id);
}
