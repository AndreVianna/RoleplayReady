using RolePlayReady.Repositories.Sphere;

namespace RolePlayReady.Handlers.Sphere;

public class SphereHandler
    : CrudHandler<Sphere, ISphereRepository>,
      ISphereHandler {
    public SphereHandler(ISphereRepository repository)
        : base(repository) {
    }
}