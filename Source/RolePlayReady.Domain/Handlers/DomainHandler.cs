namespace RolePlayReady.Handlers;

public class DomainHandler : IDomainHandler {
    private readonly IDomainRepository _repository;

    public DomainHandler(IDomainRepository repository) {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<Row>>> GetManyAsync(CancellationToken cancellation = default) {
        var list = await _repository.GetManyAsync(cancellation).ConfigureAwait(false);
        return Result.FromValue(list);
    }

    public async Task<NullableResult<Domain>> GetByIdAsync(Guid id, CancellationToken cancellation = default) {
        var output = await _repository.GetByIdAsync(id, cancellation).ConfigureAwait(false);
        if (output is null) return Result.NotFound<Domain>(nameof(id));
        return output;
    }

    public async Task<Result<Domain>> AddAsync(Domain input, CancellationToken cancellation = default) {
        var result = input.Validate();
        if (result.HasErrors) return result.WithValue(input);
        return await _repository.InsertAsync(input, cancellation).ConfigureAwait(false);
    }

    public async Task<NullableResult<Domain>> UpdateAsync(Domain input, CancellationToken cancellation = default) {
        var result = input.Validate();
        if (result.HasErrors) return result.WithValue(input);
        var output = await _repository.UpdateAsync(input, cancellation).ConfigureAwait(false);
        if (output is null) return Result.NotFound<Domain>(nameof(input));
        return output;
    }

    public Result Remove(Guid id)
        => _repository.Delete(id)
            ? Result.Success
            : Result.NotFound(nameof(id));
}