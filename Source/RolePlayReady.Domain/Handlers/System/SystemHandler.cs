using RolePlayReady.Repositories.System;

namespace RolePlayReady.Handlers.System;

public class SystemHandler
    : CrudHandler<System, SystemRow, ISystemRepository>,
      ISystemHandler {
    public SystemHandler(ISystemRepository repository)
        : base(repository) {
    }
}
