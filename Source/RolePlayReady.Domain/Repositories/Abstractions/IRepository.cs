namespace RolePlayReady.Repositories.Abstractions;

public interface IRepository<TEntity, in TKey> where TEntity : IPersistent<TKey> {
    Task<ObjectResult<IEnumerable<TEntity>>> GetManyAsync(string owner, CancellationToken cancellation = default);
    Task<NullableResult<TEntity>> GetByIdAsync(string owner, TKey id, CancellationToken cancellation = default);
    Task<ObjectResult<TEntity>> InsertAsync(string owner, TEntity input, CancellationToken cancellation = default);
    Task<ObjectResult<TEntity>> UpdateAsync(string owner, TEntity input, CancellationToken cancellation = default);
    ObjectResult<bool> Delete(string owner, TKey id);
}