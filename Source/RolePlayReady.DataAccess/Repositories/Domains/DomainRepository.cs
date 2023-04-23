namespace RolePlayReady.DataAccess.Repositories.Domains;

public class DomainRepository : IDomainRepository {
    private readonly ITrackedJsonFileRepository<DomainData> _files;

    public DomainRepository(ITrackedJsonFileRepository<DomainData> files) {
        _files = files;
    }

    public async Task<Result<IEnumerable<Row>>> GetManyAsync(string owner, CancellationToken cancellation = default) {
        var files = await _files
            .GetAllAsync(owner, string.Empty, cancellation)
            .ConfigureAwait(false);
        return files.Map(i => i.MapToRow()!);
    }

    public async Task<NullableResult<Persisted<Domain>>> GetByIdAsync(string owner, Guid id, CancellationToken cancellation = default) {
        var file = await _files
            .GetByIdAsync(owner, string.Empty, id, cancellation)
            .ConfigureAwait(false);
        return file.Map(i => i.Map());
    }

    public async Task<Result<Persisted<Domain>>> InsertAsync(string owner, Domain input, CancellationToken cancellation = default) {
        var result = await _files.UpsertAsync(owner, string.Empty, Guid.NewGuid(), input.Map(), cancellation).ConfigureAwait(false);
        return result.Map(i => i.Map()!);
    }

    public async Task<Result<Persisted<Domain>>> UpdateAsync(string owner, Guid id, Domain input, CancellationToken cancellation = default) {
        var result = await _files.UpsertAsync(owner, string.Empty, id, input.Map(), cancellation);
        return result.Map(i => i.Map()!);
    }

    public Result<bool> Delete(string owner, Guid id)
        => _files.Delete(owner, string.Empty, id);
}
