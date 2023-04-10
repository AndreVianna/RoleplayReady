using System.Results;

namespace RolePlayReady.Repositories;

public interface IRepository<TEntity, in TKey> where TEntity : IPersistent<TKey> {
    Task<Result<IEnumerable<TEntity>>> GetManyAsync(string owner, CancellationToken cancellation = default);
    Task<Maybe<TEntity>> GetByIdAsync(string owner, TKey id, CancellationToken cancellation = default);
    Task<Result<TEntity>> InsertAsync(string owner, TEntity input, CancellationToken cancellation = default);
    Task<Result<TEntity>> UpdateAsync(string owner, TEntity input, CancellationToken cancellation = default);
    Result<bool> Delete(string owner, TKey id);
}