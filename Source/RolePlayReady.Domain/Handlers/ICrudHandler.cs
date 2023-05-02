namespace RolePlayReady.Handlers;

public interface ICrudHandler<TModel>
    : ICrudHandler<TModel, Row>
    where TModel : class, IKey, IValidatable {
}

public interface ICrudHandler<TModel, TRowModel>
    where TModel : class, IKey, IValidatable
    where TRowModel : Row {
    Task<CRUDResult<IEnumerable<TRowModel>>> GetManyAsync(CancellationToken cancellation = default);
    Task<CRUDResult<TModel>> GetByIdAsync(Guid id, CancellationToken cancellation = default);
    Task<CRUDResult<TModel>> AddAsync(TModel input, CancellationToken cancellation = default);
    Task<CRUDResult<TModel>> UpdateAsync(TModel input, CancellationToken cancellation = default);
    CrudResult Remove(Guid id);
}