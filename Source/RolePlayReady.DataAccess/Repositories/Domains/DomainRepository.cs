namespace RolePlayReady.DataAccess.Repositories.Domains;

public class DomainRepository : IDomainRepository {
    private readonly ITrackedJsonFileRepository<DomainData> _files;

    public DomainRepository(ITrackedJsonFileRepository<DomainData> files) {
        _files = files;
    }

    public async Task<IEnumerable<Row>> GetManyAsync(CancellationToken cancellation = default) {
        var files = await _files
            .GetAllAsync(string.Empty, cancellation)
            .ConfigureAwait(false);
        return files.ToArray(i => i.MapToRow());
    }

    public async Task<Domain?> GetByIdAsync(Guid id, CancellationToken cancellation = default) {
        var file = await _files
            .GetByIdAsync(string.Empty, id, cancellation)
            .ConfigureAwait(false);
        return file?.Map();
    }

    public async Task<Domain> AddAsync(Domain input, CancellationToken cancellation = default) {
        var result = await _files.InsertAsync(string.Empty, input.Map(), cancellation).ConfigureAwait(false);
        return result.Map();
    }

    public async Task<Domain?> UpdateAsync(Domain input, CancellationToken cancellation = default) {
        var result = await _files.UpdateAsync(string.Empty, input.Map(), cancellation);
        return result?.Map();
    }

    public bool Remove(Guid id)
        => _files.Delete(string.Empty, id);
}
