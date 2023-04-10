using System.Results;

namespace RolePlayReady.DataAccess.Repositories;

public interface ITrackedJsonFileRepository {
    Task<Result<IEnumerable<IDataFile<TData>>>> GetAllAsync<TData>(string owner, string path, CancellationToken cancellation = default);
    Task<Maybe<IDataFile<TData>>> GetByIdAsync<TData>(string owner, string path, string id, CancellationToken cancellation = default);
    Task<Result<DateTime>> UpsertAsync<TData>(string owner, string path, string id, TData data, CancellationToken cancellation = default);
    Result<bool> Delete(string owner, string path, string id);
}
