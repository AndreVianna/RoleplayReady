namespace RolePlayReady.Repositories.Abstractions;

public interface IRepository<TEntity, in TKey> where TEntity : IPersistent<TKey> {
    Task<Object<IEnumerable<TEntity>>> GetManyAsync(string owner, CancellationToken cancellation = default);
    Task<Maybe<TEntity>> GetByIdAsync(string owner, TKey id, CancellationToken cancellation = default);
    Task<Object<TEntity>> InsertAsync(string owner, TEntity input, CancellationToken cancellation = default);
    Task<Object<TEntity>> UpdateAsync(string owner, TEntity input, CancellationToken cancellation = default);
    Object<bool> Delete(string owner, TKey id);
}