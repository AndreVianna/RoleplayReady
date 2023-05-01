namespace RolePlayReady.Repositories.Abstractions;

public interface IRepository<TEntity, TEntityRow>
    where TEntity : class
    where TEntityRow : Row {
    Task<IEnumerable<TEntityRow>> GetManyAsync(CancellationToken cancellation = default);
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellation = default);
    Task<TEntity> AddAsync(TEntity input, CancellationToken cancellation = default);
    Task<TEntity?> UpdateAsync(TEntity input, CancellationToken cancellation = default);
    bool Remove(Guid id);
}