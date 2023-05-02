namespace RolePlayReady.DataAccess.Repositories.Domains;

public class SphereRepository : ISphereRepository {
    private readonly IJsonFileStorage<SphereData> _files;

    public SphereRepository(IJsonFileStorage<SphereData> files, IUserAccessor owner) {
        _files = files;
        files.SetBasePath($"{owner.BaseFolder}/Spheres/Domains");
    }

    public async Task<IEnumerable<Row>> GetManyAsync(CancellationToken cancellation = default) {
        var files = await _files
            .GetAllAsync(cancellation)
            .ConfigureAwait(false);
        return files.ToArray(SphereMapper.ToRow);
    }

    public async Task<Sphere?> GetByIdAsync(Guid id, CancellationToken cancellation = default) {
        var file = await _files
            .GetByIdAsync(id, cancellation)
            .ConfigureAwait(false);
        return file.ToModel();
    }

    public async Task<Sphere?> AddAsync(Sphere input, CancellationToken cancellation = default) {
        var file = await _files.CreateAsync(input.ToData(), cancellation).ConfigureAwait(false);
        return file.ToModel();
    }

    public async Task<Sphere?> UpdateAsync(Sphere input, CancellationToken cancellation = default) {
        var file = await _files.UpdateAsync(input.ToData(), cancellation);
        return file.ToModel();
    }

    public bool Remove(Guid id)
        => _files.Delete(id);
}
