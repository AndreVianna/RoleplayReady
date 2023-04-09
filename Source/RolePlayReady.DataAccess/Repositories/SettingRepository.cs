namespace RolePlayReady.DataAccess.Repositories;

public class SettingRepository : ISettingRepository {
    private readonly IDataFileRepository _files;

    public SettingRepository(IDataFileRepository files) {
        _files = files;
    }

    public async Task<IEnumerable<Setting>> GetManyAsync(CancellationToken cancellation = default) {
        var files = await _files
            .GetAllAsync<SettingDataModel>(null, cancellation)
            .ConfigureAwait(false);
        return files.Select(SettingMapper.MapFrom).ToArray()!;
    }

    public async Task<Setting?> GetByIdAsync(string id, CancellationToken cancellation = default) {
        var file = await _files
            .GetByIdAsync<SettingDataModel>(null, id, cancellation)
            .ConfigureAwait(false);
        return SettingMapper.MapFrom(file);
    }

    public Task UpsertAsync(Setting setting, CancellationToken cancellation = default)
        => _files.UpsertAsync(null, setting.ShortName, SettingMapper.MapFrom(setting), cancellation);

    public void Delete(string id)
        => _files.Delete(null, id);
}
