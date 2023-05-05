using System.Validation.Abstractions;

namespace RolePlayReady.Handlers;

public interface ICrudHandler<TModel>
    : ICrudHandler<TModel, Row>
    where TModel : class, IKey, IValidatable {
}

public interface ICrudHandler<TModel, TRowModel>
    where TModel : class, IKey, IValidatable
    where TRowModel : Row {
    Task<CrudResult<IEnumerable<TRowModel>>> GetManyAsync(CancellationToken cancellation = default);
    Task<CrudResult<TModel>> GetByIdAsync(Guid id, CancellationToken cancellation = default);
    Task<CrudResult<TModel>> AddAsync(TModel input, CancellationToken cancellation = default);
    Task<CrudResult<TModel>> UpdateAsync(TModel input, CancellationToken cancellation = default);
    CrudResult Remove(Guid id);
}