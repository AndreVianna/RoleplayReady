namespace RolePlayReady.Repositories;

public interface IRepository<TModel>
    : IRepository<TModel, Row>
    where TModel : class, IKey {
}

public interface IRepository<TModel, TRowModel>
    where TModel : class, IKey
    where TRowModel : Row {
    Task<IEnumerable<TRowModel>> GetManyAsync(CancellationToken cancellation = default);
    Task<TModel?> GetByIdAsync(Guid id, CancellationToken cancellation = default);
    Task<TModel?> AddAsync(TModel input, CancellationToken cancellation = default);
    Task<TModel?> UpdateAsync(TModel input, CancellationToken cancellation = default);
    bool Remove(Guid id);
}