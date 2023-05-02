using RolePlayReady.Repositories;

namespace RolePlayReady.DataAccess.Repositories;

public class Repository<TModel, TRowModel, TData>
    : IRepository<TModel, TRowModel>
    where TModel : class, IKey
    where TRowModel : Row
    where TData : class, IKey {
    private readonly IJsonFileHandler<TData> _files;
    private readonly IDataMapper<TModel, TRowModel, TData> _map;

    public Repository(IJsonFileHandler<TData> files, IDataMapper<TModel, TRowModel, TData> map) {
        _files = files;
        _map = map;
    }

    public async Task<IEnumerable<TRowModel>> GetManyAsync(CancellationToken cancellation = default) {
        var files = await _files
            .GetAllAsync(cancellation)
            .ConfigureAwait(false);
        return files.ToArray(_map.ToRow);
    }

    public async Task<TModel?> GetByIdAsync(Guid id, CancellationToken cancellation = default) {
        var file = await _files
            .GetByIdAsync(id, cancellation)
            .ConfigureAwait(false);
        return _map.ToModel(file);
    }

    public async Task<TModel?> AddAsync(TModel input, CancellationToken cancellation = default) {
        var file = await _files.CreateAsync(_map.ToData(input), cancellation).ConfigureAwait(false);
        return _map.ToModel(file);
    }

    public async Task<TModel?> UpdateAsync(TModel input, CancellationToken cancellation = default) {
        var file = await _files.UpdateAsync(_map.ToData(input), cancellation);
        return _map.ToModel(file);
    }

    public bool Remove(Guid id)
        => _files.Delete(id);
}