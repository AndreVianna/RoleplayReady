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

    public async Task<SearchResult<Domain?>> GetByIdAsync(Guid id, CancellationToken cancellation = default) {
        var output = await _repository.GetByIdAsync(id, cancellation).ConfigureAwait(false);
        return output is not null
            ? SearchResult.FromValue<Domain?>(output)
            : SearchResult.NotFound(output);
    }

    public async Task<Result<Domain>> AddAsync(Domain input, CancellationToken cancellation = default) {
        var result = input.Validate();
        return result.HasErrors
            ? result.ToResult(input)
            : await _repository.InsertAsync(input, cancellation).ConfigureAwait(false);
    }

    public async Task<SearchResult<Domain>> UpdateAsync(Domain input, CancellationToken cancellation = default) {
        var result = input.Validate();
        if (result.HasErrors) return result.ToSearchResult(input);
        var output = await _repository.UpdateAsync(input, cancellation).ConfigureAwait(false);
        return output is not null
            ? SearchResult.FromValue(output)
            : SearchResult.NotFound(input);
    }

    public SearchResult Remove(Guid id)
        => _repository.Delete(id)
            ? SearchResult.Success
            : SearchResult.NotFound();
}