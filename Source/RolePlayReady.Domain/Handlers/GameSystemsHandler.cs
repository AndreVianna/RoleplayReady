using RolePlayReady.Repositories.Abstractions;
using RolePlayReady.Security.Abstractions;

namespace RolePlayReady.Handlers;

public class GameSystemsHandler {
    private readonly IGameSystemsRepository _repository;
    private readonly string _owner;

    public GameSystemsHandler(IGameSystemsRepository repository, IUserAccessor user) {
        _repository = repository;
        _owner = user.Id;
    }

    public async Task<Object<IEnumerable<GameSystem>>> GetManyAsync(CancellationToken cancellation = default)
        => await _repository.GetManyAsync(_owner, cancellation);

    public async Task<Maybe<GameSystem>> GetByIdAsync(Guid id, CancellationToken cancellation = default)
        => await _repository.GetByIdAsync(_owner, id, cancellation);

    public async Task<Object<GameSystem>> AddAsync(GameSystem input, CancellationToken cancellation = default) {
        var result = input.Validate();
        return result.HasErrors
            ? await _repository.InsertAsync(_owner, input, cancellation)
            : throw new("Validation failed.");
    }

    public async Task<Object<GameSystem>> UpdateAsync(GameSystem input, CancellationToken cancellation = default)
        // Add validations here.
        => await _repository.UpdateAsync(_owner, input, cancellation);

    public Object<bool> Remove(Guid id)
        => _repository.Delete(_owner, id);
}