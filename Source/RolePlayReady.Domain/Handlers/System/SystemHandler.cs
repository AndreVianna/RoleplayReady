using RolePlayReady.Repositories.System;

namespace RolePlayReady.Handlers.System;

public class SystemHandler
    : CrudHandler<System, ISystemRepository>,
      ISystemHandler {
    public SystemHandler(ISystemRepository repository)
        : base(repository) {
    }
}
