using static System.Results.CrudResult;

namespace RolePlayReady.Handlers;

public class CrudHandler<TModel, TRepository>
    : CrudHandler<TModel, Row, TRepository>,
      ICrudHandler<TModel>
    where TModel : class, IKey, IValidatable
    where TRepository : IRepository<TModel> {
    public CrudHandler(TRepository repository)
        : base(repository) {
    }
}

public class CrudHandler<TModel, TRowModel, TRepository>
    : ICrudHandler<TModel, TRowModel>
    where TModel : class, IKey, IValidatable
    where TRowModel : Row
    where TRepository : IRepository<TModel, TRowModel> {

    public CrudHandler(TRepository repository) {
        Repository = repository;
    }

    protected TRepository Repository { get; }

    public async Task<CrudResult<IEnumerable<TRowModel>>> GetManyAsync(CancellationToken cancellation = default) {
        var list = await Repository.GetManyAsync(cancellation).ConfigureAwait(false);
        return Success(list);
    }

    public async Task<CrudResult<TModel>> GetByIdAsync(Guid id, CancellationToken cancellation = default) {
        var output = await Repository.GetByIdAsync(id, cancellation).ConfigureAwait(false);
        return output is not null
            ? Success(output)
            : NotFound(output);
    }

    public async Task<CrudResult<TModel>> AddAsync(TModel input, CancellationToken cancellation = default) {
        var validation = input.Validate();
        if (validation.IsInvalid) return Invalid(input, validation.Errors);
        var output = await Repository.AddAsync(input, cancellation).ConfigureAwait(false);
        return output is null
            ? Conflict(input)
            : Success(output);
    }

    public async Task<CrudResult<TModel>> UpdateAsync(TModel input, CancellationToken cancellation = default) {
        var validation = input.Validate();
        if (validation.IsInvalid) return Invalid(input, validation.Errors);
        var output = await Repository.UpdateAsync(input, cancellation).ConfigureAwait(false);
        return output is null
            ? NotFound(input)
            : Success(output);
    }

    public CrudResult Remove(Guid id)
        => Repository.Remove(id)
            ? Success()
            : NotFound();
}
