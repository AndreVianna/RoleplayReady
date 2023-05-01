using static System.Results.CRUDResult;

namespace RolePlayReady.Handlers;

public class GameSystemHandler : IGameSystemHandler {
    private readonly IGameSystemRepository _repository;

    public GameSystemHandler(IGameSystemRepository repository) {
        _repository = repository;
    }

    public async Task<CRUDResult<IEnumerable<Row>>> GetManyAsync(CancellationToken cancellation = default) {
        var list = await _repository.GetManyAsync(cancellation).ConfigureAwait(false);
        return AsSuccessFor(list);
    }

    public async Task<CRUDResult<GameSystem>> GetByIdAsync(Guid id, CancellationToken cancellation = default) {
        var output = await _repository.GetByIdAsync(id, cancellation);
        return output is not null
            ? AsSuccessFor(output)
            : AsNotFoundFor(output);
    }

    public async Task<CRUDResult<GameSystem>> AddAsync(GameSystem input, CancellationToken cancellation = default) {
        var validation = input.Validate();
        if (validation.HasErrors) return validation.ToCRUDResult(input);
        var output = await _repository.AddAsync(input, cancellation).ConfigureAwait(false);
        return output is not null
            ? AsSuccessFor(output)
            : AsConflictFor(input);
    }

    public async Task<CRUDResult<GameSystem>> UpdateAsync(GameSystem input, CancellationToken cancellation = default) {
        var validation = input.Validate();
        if (validation.HasErrors) return AsInvalidFor(input, validation.Errors);
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