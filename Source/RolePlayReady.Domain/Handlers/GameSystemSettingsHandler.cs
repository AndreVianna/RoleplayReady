using RolePlayReady.Repositories.Abstractions;
using RolePlayReady.Security.Abstractions;

namespace RolePlayReady.Handlers;

public class GameSystemSettingsHandler {
    private readonly IGameSystemSettingsRepository _repository;
    private readonly string _owner;

    public GameSystemSettingsHandler(IGameSystemSettingsRepository repository, IUserAccessor user) {
        _repository = repository;
        _owner = user.Id;
    }

    public async Task<Object<IEnumerable<GameSystemSetting>>> GetManyAsync(CancellationToken cancellation = default)
        // Add validations here.
        => await _repository.GetManyAsync(_owner, cancellation);

    public async Task<Maybe<GameSystemSetting>> GetByIdAsync(Guid id, CancellationToken cancellation = default)
        // Add validations here.
        => await _repository.GetByIdAsync(_owner, id, cancellation);

    public async Task<Object<GameSystemSetting>> AddAsync(GameSystemSetting input, CancellationToken cancellation = default)
        // Add validations here.
        => await _repository.InsertAsync(_owner, input, cancellation);

    public async Task<Object<GameSystemSetting>> UpdateAsync(GameSystemSetting input, CancellationToken cancellation = default)
        // Add validations here.
        => await _repository.UpdateAsync(_owner, input, cancellation);

    public Object<bool> Remove(Guid id)
        // Add validations here.
        => _repository.Delete(_owner, id);
}