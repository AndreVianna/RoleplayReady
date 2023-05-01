namespace RolePlayReady.DataAccess.Repositories.Domains;

public class DomainRepository : Repository<Domain, Row, DomainData>, IDomainRepository {
    public DomainRepository(IJsonFileHandler<DomainData> files, DomainMapper mapper, IUserAccessor owner)
        : base(files, mapper) {
        files.SetBasePath($"{owner.BaseFolder}/GameSystems/Domains");
    }
}
