using GameSystem = RolePlayReady.Handlers.System.System;
using GameSystemRow = RolePlayReady.Handlers.System.SystemRow;

namespace RolePlayReady.Repositories.System;

public interface ISystemRepository
    : IRepository<GameSystem, GameSystemRow> {
}
