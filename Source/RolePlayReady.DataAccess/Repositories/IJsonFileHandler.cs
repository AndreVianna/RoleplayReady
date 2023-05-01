namespace RolePlayReady.DataAccess.Repositories;

public interface IJsonFileHandler<TData>
    where TData : class, IKey {
    void SetBasePath(string path);
    Task<IEnumerable<TData>> GetAllAsync(CancellationToken cancellation = default);
    Task<TData?> GetByIdAsync(Guid id, CancellationToken cancellation = default);
    Task<TData?> CreateAsync(TData data, CancellationToken cancellation = default);
    Task<TData?> UpdateAsync(TData data, CancellationToken cancellation = default);
    bool Delete(Guid id);
}
