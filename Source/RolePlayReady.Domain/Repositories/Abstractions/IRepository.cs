namespace RolePlayReady.Repositories.Abstractions;

public interface IRepository<TEntity, in TKey> where TEntity : IPersistent<TKey> {
    Task<ResultOf<IEnumerable<TEntity>>> GetManyAsync(string owner, CancellationToken cancellation = default);
    Task<ResultOf<TEntity>> GetByIdAsync(string owner, TKey id, CancellationToken cancellation = default);
    Task<ResultOf<TEntity>> InsertAsync(string owner, TEntity input, CancellationToken cancellation = default);
    Task<ResultOf<TEntity>> UpdateAsync(string owner, TEntity input, CancellationToken cancellation = default);
    ResultOf<bool> Delete(string owner, TKey id);
}