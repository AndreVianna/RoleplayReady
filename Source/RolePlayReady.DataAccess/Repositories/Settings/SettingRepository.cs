namespace RolePlayReady.DataAccess.Repositories.Settings;

public class SettingRepository : ISettingRepository {
    private readonly IJsonFileStorage<SettingData> _files;

    public SettingRepository(IJsonFileStorage<SettingData> files, IUserAccessor owner) {
        _files = files;
        files.SetBasePath($"{owner.BaseFolder}/Settings");
    }

    public async Task<IEnumerable<SettingRow>> GetManyAsync(CancellationToken cancellation = default) {
        var files = await _files
            .GetAllAsync(cancellation: cancellation)
            .ConfigureAwait(false);
        return files.ToArray(SettingMapper.ToRow);
    }

    public async Task<Setting?> GetByIdAsync(Guid id, CancellationToken cancellation = default) {
        var file = await _files
            .GetByIdAsync(id, cancellation)
            .ConfigureAwait(false);
        return file.ToModel();
    }

    public async Task<Setting?> AddAsync(Setting input, CancellationToken cancellation = default) {
        var file = await _files.CreateAsync(input.ToData(), cancellation).ConfigureAwait(false);
        return file.ToModel();
    }

    public async Task<Setting?> UpdateAsync(Setting input, CancellationToken cancellation = default) {
        var file = await _files.UpdateAsync(input.ToData(), cancellation);
        return file.ToModel();
    }

    public bool Remove(Guid id)
        => _files.Delete(id);
}
