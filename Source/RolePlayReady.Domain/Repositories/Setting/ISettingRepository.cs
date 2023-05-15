using GameSetting = RolePlayReady.Handlers.Setting.Setting;
using GameSettingRow = RolePlayReady.Handlers.Setting.SettingRow;

namespace RolePlayReady.Repositories.Setting;

public interface ISettingRepository
    : IRepository<GameSetting, GameSettingRow> {
}
