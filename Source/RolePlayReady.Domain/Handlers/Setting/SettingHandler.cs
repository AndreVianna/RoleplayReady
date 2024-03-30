using RolePlayReady.Repositories.Setting;

namespace RolePlayReady.Handlers.Setting;

public class SettingHandler(ISettingRepository repository)
        : CrudHandler<Setting, SettingRow, ISettingRepository>(repository),
      ISettingHandler {
}