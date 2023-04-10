using System.Results;

namespace RolePlayReady.DataAccess.Services;

public class SettingService {
    private readonly IGameSystemSettingsRepository _gameSystemSettings;
    private readonly string _owner;

    public SettingService(IGameSystemSettingsRepository gameSystemSettings, IUserAccessor user) {
        _gameSystemSettings = gameSystemSettings;
        _owner = user.Id;
    }

    public async Task<Maybe<GameSystemSetting>> LoadAsync(Guid id, CancellationToken cancellation = default)
        // Add validations here.
        => await _gameSystemSettings.GetByIdAsync(_owner, id, cancellation);
}