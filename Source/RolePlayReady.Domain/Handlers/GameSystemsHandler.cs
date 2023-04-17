namespace RolePlayReady.Handlers;

public class GameSystemsHandler {
    private readonly IGameSystemsRepository _repository;
    private readonly string _owner;

    public GameSystemsHandler(IGameSystemsRepository repository, IUserAccessor user) {
        _repository = repository;
        _owner = user.Id;
    }

    public async Task<Result<IEnumerable<GameSystem>>> GetManyAsync(CancellationToken cancellation = default)
        => await _repository.GetManyAsync(_owner, cancellation);

    public async Task<NullableResult<GameSystem>> GetByIdAsync(Guid id, CancellationToken cancellation = default)
        => await _repository.GetByIdAsync(_owner, id, cancellation);

    public async Task<Result<GameSystem>> AddAsync(GameSystem input, CancellationToken cancellation = default) {
        var result = input.Validate();
        return result.IsSuccessful
            ? await _repository.InsertAsync(_owner, input, cancellation)
            : result + input;
    }

    public async Task<Result<GameSystem>> UpdateAsync(GameSystem input, CancellationToken cancellation = default) {
        var result = input.Validate();
        return result.IsSuccessful
            ? await _repository.UpdateAsync(_owner, input, cancellation)
            : result + input;
    }

    public Result<bool> Remove(Guid id)
        => _repository.Delete(_owner, id);
}