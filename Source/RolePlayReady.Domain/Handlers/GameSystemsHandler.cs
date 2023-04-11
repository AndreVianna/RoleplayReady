namespace RolePlayReady.Handlers;

public class GameSystemsHandler {
    private readonly IGameSystemsRepository _repository;
    private readonly string _owner;

    public GameSystemsHandler(IGameSystemsRepository repository, IUserAccessor user) {
        _repository = repository;
        _owner = user.Id;
    }

    public async Task<Result<IEnumerable<GameSystem>>> GetManyAsync(CancellationToken cancellation = default)
        // Add validations here.
        => await _repository.GetManyAsync(_owner, cancellation);

    public async Task<Maybe<GameSystem>> GetByIdAsync(Guid id, CancellationToken cancellation = default)
        // Add validations here.
        => await _repository.GetByIdAsync(_owner, id, cancellation);

    public async Task<Result<GameSystem>> AddAsync(GameSystem input, CancellationToken cancellation = default)
        // Add validations here.
        => await _repository.InsertAsync(_owner, input, cancellation);

    public async Task<Result<GameSystem>> UpdateAsync(GameSystem input, CancellationToken cancellation = default)
        // Add validations here.
        => await _repository.UpdateAsync(_owner, input, cancellation);

    public Result<bool> Remove(Guid id)
        // Add validations here.
        => _repository.Delete(_owner, id);
}