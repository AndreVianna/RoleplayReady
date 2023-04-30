namespace RolePlayReady.Handlers;

public class GameSystemHandler : IGameSystemHandler {
    private readonly IGameSystemRepository _repository;

    public GameSystemHandler(IGameSystemRepository repository) {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<Row>>> GetManyAsync(CancellationToken cancellation = default) {
        var list = await _repository.GetManyAsync(cancellation).ConfigureAwait(false);
        return Result.FromValue(list);
    }

    public async Task<SearchResult<GameSystem?>> GetByIdAsync(Guid id, CancellationToken cancellation = default) {
        var output = await _repository.GetByIdAsync(id, cancellation);
        return output is not null
            ? SearchResult.FromValue<GameSystem?>(output)
            : SearchResult.NotFound(output);
    }

    public async Task<Result<GameSystem>> AddAsync(GameSystem input, CancellationToken cancellation = default) {
        var result = input.Validate();
        return result.HasErrors
            ? result.ToResult(input)
            : await _repository.InsertAsync(input, cancellation);
    }

    public async Task<SearchResult<GameSystem>> UpdateAsync(GameSystem input, CancellationToken cancellation = default) {
        var result = input.Validate();
        if (result.HasErrors) return result.ToSearchResult(input);
        var output = await _repository.UpdateAsync(input, cancellation);
        return output is not null
            ? SearchResult.FromValue(output)
            : SearchResult.NotFound(input);
    }

    public SearchResult Remove(Guid id)
        => _repository.Delete(id)
            ? SearchResult.Success
            : SearchResult.NotFound();
}