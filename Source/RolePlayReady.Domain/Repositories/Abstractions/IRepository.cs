namespace RolePlayReady.Repositories.Abstractions;

public interface IRepository<TEntity, TEntityRow>
    where TEntity : class
    where TEntityRow : Row {
    Task<Result<IEnumerable<TEntityRow>>> GetManyAsync(string owner, CancellationToken cancellation = default);
    Task<NullableResult<Persisted<TEntity>>> GetByIdAsync(string owner, Guid id, CancellationToken cancellation = default);
    Task<Result<Persisted<TEntity>>> InsertAsync(string owner, TEntity input, CancellationToken cancellation = default);
    Task<Result<Persisted<TEntity>>> UpdateAsync(string owner, Guid id, TEntity input, CancellationToken cancellation = default);
    Result<bool> Delete(string owner, Guid id);
}