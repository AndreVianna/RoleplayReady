using RolePlayReady.Repositories.System;

namespace RolePlayReady.Handlers.System;

public class SystemHandler(ISystemRepository repository)
        : CrudHandler<System, SystemRow, ISystemRepository>(repository),
      ISystemHandler {
}
