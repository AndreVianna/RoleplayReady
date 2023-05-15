using RolePlayReady.Repositories.Setting;

namespace RolePlayReady.Handlers.Setting;

public class SettingHandler
    : CrudHandler<Setting, SettingRow, ISettingRepository>,
      ISettingHandler {
    public SettingHandler(ISettingRepository repository)
        : base(repository) {
    }
}