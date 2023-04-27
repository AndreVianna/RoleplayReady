namespace RolePlayReady.Handlers;

public class GameSystemHandler : IGameSystemHandler {
    private readonly IGameSystemRepository _repository;
    private readonly string _owner;

    public GameSystemHandler(IGameSystemRepository repository, IUserAccessor user) {
        _repository = repository;
        _owner = user.Id;
    }

    public async Task<Result<IEnumerable<Row>>> GetManyAsync(CancellationToken cancellation = default) {
        var list = await _repository.GetManyAsync(_owner, cancellation).ConfigureAwait(false);
        return new(list);
    }

    public async Task<NullableResult<GameSystem>> GetByIdAsync(Guid id, CancellationToken cancellation = default)
        => await _repository.GetByIdAsync(_owner, id, cancellation);

    public async Task<Result<GameSystem>> AddAsync(GameSystem input, CancellationToken cancellation = default) {
        var result = input.Validate();
        return result.IsSuccess
            ? await _repository.InsertAsync(_owner, input, cancellation)
            : result.WithValue(input);
    }

    public async Task<NullableResult<GameSystem>> UpdateAsync(GameSystem input, CancellationToken cancellation = default) {
        var result = input.Validate();
        return result.IsSuccess
            ? await _repository.UpdateAsync(_owner, input, cancellation)
            : result.WithValue(input);
    }

    public Result Remove(Guid id)
        => _repository.Delete(_owner, id);
}