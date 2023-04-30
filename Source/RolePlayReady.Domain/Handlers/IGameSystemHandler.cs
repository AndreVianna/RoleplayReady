namespace RolePlayReady.Handlers;

public interface IGameSystemHandler {
    Task<Result<IEnumerable<Row>>> GetManyAsync(CancellationToken cancellation = default);
    Task<SearchResult<GameSystem?>> GetByIdAsync(Guid id, CancellationToken cancellation = default);
    Task<Result<GameSystem>> AddAsync(GameSystem input, CancellationToken cancellation = default);
    Task<SearchResult<GameSystem>> UpdateAsync(GameSystem input, CancellationToken cancellation = default);
    SearchResult Remove(Guid id);
}