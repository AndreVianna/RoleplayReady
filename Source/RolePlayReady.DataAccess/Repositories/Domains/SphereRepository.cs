using RolePlayReady.Handlers.Sphere;

namespace RolePlayReady.DataAccess.Repositories.Domains;

public class SphereRepository : Repository<Sphere, Row, SphereData>, ISphereRepository {
    public SphereRepository(IJsonFileHandler<SphereData> files, ISphereMapper mapper, IUserAccessor owner)
        : base(files, mapper) {
        files.SetBasePath($"{owner.BaseFolder}/GameSystems/Domains");
    }
}
