namespace RolePlayReady.DataAccess.Repositories;

public interface ITrackedJsonFileRepository<TData>
    where TData : class, IKey {
    Task<IEnumerable<TData>> GetAllAsync(string path, CancellationToken cancellation = default);
    Task<TData?> GetByIdAsync(string path, Guid id, CancellationToken cancellation = default);
    Task<TData> InsertAsync(string path, TData data, CancellationToken cancellation = default);
    Task<TData?> UpdateAsync(string path, TData data, CancellationToken cancellation = default);
    bool Delete(string path, Guid id);
}
