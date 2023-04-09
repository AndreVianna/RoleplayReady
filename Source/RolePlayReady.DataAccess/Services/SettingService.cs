namespace RolePlayReady.DataAccess.Services;

public class SettingService {
    private readonly ISettingRepository _settings;

    public SettingService(ISettingRepository settings) {
        _settings = settings;
    }

    public async Task<Setting> LoadAsync(string id, CancellationToken cancellation = default)
        => await _settings.GetByIdAsync(id, cancellation)
           ?? throw new InvalidOperationException($"Rule set for '{id}' was not found.");
}
