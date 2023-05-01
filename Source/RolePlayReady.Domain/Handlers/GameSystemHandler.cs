﻿namespace RolePlayReady.Handlers;

public class GameSystemHandler : IGameSystemHandler {
    private readonly IGameSystemRepository _repository;

    public GameSystemHandler(IGameSystemRepository repository) {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<Row>>> GetManyAsync(CancellationToken cancellation = default) {
        var list = await _repository.GetManyAsync(cancellation).ConfigureAwait(false);
        return Result.AsSuccessFor(list);
    }

    public async Task<ResultOrNotFound<GameSystem>> GetByIdAsync(Guid id, CancellationToken cancellation = default) {
        var output = await _repository.GetByIdAsync(id, cancellation);
        return output is not null
            ? ResultOrNotFound.AsSuccessFor(output)
            : ResultOrNotFound.AsNotFoundFor(output)!;
    }

    public async Task<Result<GameSystem>> AddAsync(GameSystem input, CancellationToken cancellation = default) {
        var result = input.Validate();
        return result.HasErrors
            ? result.ToResult(input)
            : await _repository.AddAsync(input, cancellation);
    }

    public async Task<ResultOrNotFound<GameSystem>> UpdateAsync(GameSystem input, CancellationToken cancellation = default) {
        var result = input.Validate();
        if (result.HasErrors) return ResultOrNotFound.AsInvalidFor(input, result.Errors);
        var output = await _repository.UpdateAsync(input, cancellation);
        return output is not null
            ? ResultOrNotFound.AsSuccessFor(output)
            : ResultOrNotFound.AsNotFoundFor(input)!;
    }

    public ResultOrNotFound Remove(Guid id)
        => _repository.Remove(id)
            ? ResultOrNotFound.AsSuccess()
            : ResultOrNotFound.AsNotFound();
}