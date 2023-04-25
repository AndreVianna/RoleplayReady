namespace RolePlayReady.Repositories.Abstractions;

public interface IRepository<TEntity, TEntityRow>
    where TEntity : class
    where TEntityRow : Row {
    Task<IEnumerable<TEntityRow>> GetManyAsync(string owner, CancellationToken cancellation = default);
    Task<TEntity?> GetByIdAsync(string owner, Guid id, CancellationToken cancellation = default);
    Task<TEntity> InsertAsync(string owner, TEntity input, CancellationToken cancellation = default);
    Task<TEntity?> UpdateAsync(string owner, TEntity input, CancellationToken cancellation = default);
    FlagResult Delete(string owner, Guid id);
}