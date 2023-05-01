using static System.Results.CRUDResult;

namespace RolePlayReady.Handlers;

public class DomainHandler : IDomainHandler {
    private readonly IDomainRepository _repository;

    public DomainHandler(IDomainRepository repository) {
        _repository = repository;
    }

    public async Task<CRUDResult<IEnumerable<Row>>> GetManyAsync(CancellationToken cancellation = default) {
        var list = await _repository.GetManyAsync(cancellation).ConfigureAwait(false);
        return AsSuccessFor(list);
    }

    public async Task<CRUDResult<Domain>> GetByIdAsync(Guid id, CancellationToken cancellation = default) {
        var output = await _repository.GetByIdAsync(id, cancellation).ConfigureAwait(false);
        return output is not null
            ? AsSuccessFor(output)
            : AsNotFoundFor(output);
    }

    public async Task<CRUDResult<Domain>> AddAsync(Domain input, CancellationToken cancellation = default) {
        var result = input.Validate();
        if (result.HasErrors) return result.ToCRUDResult(input);
        var output = await _repository.AddAsync(input, cancellation).ConfigureAwait(false);
        return output is not null
            ? AsSuccessFor(output)
            : AsConflictFor(input);
    }

    public async Task<CRUDResult<Domain>> UpdateAsync(Domain input, CancellationToken cancellation = default) {
        var result = input.Validate();
        if (result.HasErrors) return result.ToCRUDResult(input);
        var output = await _repository.UpdateAsync(input, cancellation).ConfigureAwait(false);
        return output is not null
            ? AsSuccessFor(output)
            : AsNotFoundFor(input);
    }

    public CRUDResult Remove(Guid id)
        => _repository.Remove(id)
            ? AsSuccess()
            : AsNotFound();
}