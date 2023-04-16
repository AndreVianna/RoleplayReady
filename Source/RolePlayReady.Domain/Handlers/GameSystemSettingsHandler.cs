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

    public async Task<ObjectResult<IEnumerable<GameSystemSetting>>> GetManyAsync(CancellationToken cancellation = default)
        => await _repository.GetManyAsync(_owner, cancellation);

    public async Task<NullableResult<GameSystemSetting>> GetByIdAsync(Guid id, CancellationToken cancellation = default)
        => await _repository.GetByIdAsync(_owner, id, cancellation);

    public async Task<ObjectResult<GameSystemSetting>> AddAsync(GameSystemSetting input, CancellationToken cancellation = default) {
        var result = input.Validate();
        return result.IsSuccessful
            ? await _repository.InsertAsync(_owner, input, cancellation)
            : result;
    }

    public async Task<ObjectResult<GameSystemSetting>> UpdateAsync(GameSystemSetting input, CancellationToken cancellation = default) {
        var result = input.Validate();
        return result.IsSuccessful
            ? await _repository.UpdateAsync(_owner, input, cancellation)
            : result;
    }

    public ObjectResult<bool> Remove(Guid id)
        => _repository.Delete(_owner, id);
}