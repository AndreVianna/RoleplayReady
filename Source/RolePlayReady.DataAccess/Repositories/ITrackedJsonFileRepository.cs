namespace RolePlayReady.DataAccess.Repositories;

public interface ITrackedJsonFileRepository<TData>
    where TData : class {
    Task<Result<IEnumerable<Persisted<TData>>>> GetAllAsync(string owner, string path, CancellationToken cancellation = default);
    Task<NullableResult<Persisted<TData>>> GetByIdAsync(string owner, string path, Guid id, CancellationToken cancellation = default);
    Task<Result<Persisted<TData>>> UpsertAsync(string owner, string path, Guid id, TData data, CancellationToken cancellation = default);
    Result<bool> Delete(string owner, string path, Guid id);
}
