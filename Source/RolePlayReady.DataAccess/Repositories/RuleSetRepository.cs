namespace RolePlayReady.DataAccess.Repositories;

public class RuleSetRepository : IRuleSetRepository {
    private readonly IDataFileRepository _files;

    public RuleSetRepository(IDataFileRepository files) {
        _files = files;
    }

    public async Task<IEnumerable<RuleSet>> GetManyAsync(CancellationToken cancellation = default) {
        var files = await _files
            .GetAllAsync<RuleSetDataModel>(cancellation)
            .ConfigureAwait(false);
        return files.Select(RuleSetMapper.MapFrom).ToArray()!;
    }

    public async Task<RuleSet?> GetByIdAsync(string id, CancellationToken cancellation = default) {
        var file = await _files
            .GetByIdAsync<RuleSetDataModel>(id, cancellation)
            .ConfigureAwait(false);
        return RuleSetMapper.MapFrom(file);
    }

    public Task UpsertAsync(RuleSet ruleSet, CancellationToken cancellation = default)
        => _files.UpsertAsync(ruleSet.ShortName, RuleSetMapper.MapFrom(ruleSet), cancellation);

    public void Delete(string id)
        => _files.Delete(id);
}
