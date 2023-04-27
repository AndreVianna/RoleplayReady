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

    public async Task<NullableResult<GameSystem>> GetByIdAsync(Guid id, CancellationToken cancellation = default) {
        var output = await _repository.GetByIdAsync(id, cancellation);
        if (output is not null) return output;
        return Result.NotFound<GameSystem>(nameof(id));
    }

    public async Task<Result<GameSystem>> AddAsync(GameSystem input, CancellationToken cancellation = default) {
        var result = input.Validate();
        if (result.HasErrors) return result.WithValue(input);
        return await _repository.InsertAsync(input, cancellation);
    }

    public async Task<NullableResult<GameSystem>> UpdateAsync(GameSystem input, CancellationToken cancellation = default) {
        var result = input.Validate();
        if (result.HasErrors) return result.WithValue(input);
        var output = await _repository.UpdateAsync(input, cancellation);
        if (output is not null) return output;
        return Result.NotFound<GameSystem>(nameof(input));
    }

    public Result Remove(Guid id)
        => _repository.Delete(id)
            ? Result.Success
            : Result.NotFound(nameof(id));
}