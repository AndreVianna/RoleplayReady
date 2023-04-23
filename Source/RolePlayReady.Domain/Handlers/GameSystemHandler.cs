namespace RolePlayReady.Handlers;

public class GameSystemHandler {
    private readonly IGameSystemsRepository _repository;
    private readonly string _owner;

    public GameSystemHandler(IGameSystemsRepository repository, IUserAccessor user) {
        _repository = repository;
        _owner = user.Id;
    }

    public async Task<Result<IEnumerable<Row>>> GetManyAsync(CancellationToken cancellation = default)
        => await _repository.GetManyAsync(_owner, cancellation);

    public async Task<NullableResult<Persisted<GameSystem>>> GetByIdAsync(Guid id, CancellationToken cancellation = default)
        => await _repository.GetByIdAsync(_owner, id, cancellation);

    public async Task<Result<Persisted<GameSystem>>> AddAsync(GameSystem input, CancellationToken cancellation = default) {
        var result = input.Validate();
        return result.IsSuccess
            ? await _repository.InsertAsync(_owner, input, cancellation)
            : result + new Persisted<GameSystem> {
                Id = Guid.Empty,
                Content = input,
            };
    }

    public async Task<Result<Persisted<GameSystem>>> UpdateAsync(Guid id, GameSystem input, CancellationToken cancellation = default) {
        var result = input.Validate();
        return result.IsSuccess
            ? await _repository.UpdateAsync(_owner, id, input, cancellation)
            : result + new Persisted<GameSystem> {
                Id = Guid.Empty,
                Content = input,
            };
    }

    public Result<bool> Remove(Guid id)
        => _repository.Delete(_owner, id);
}