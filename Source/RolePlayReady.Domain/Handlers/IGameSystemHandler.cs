namespace RolePlayReady.Handlers;

public interface IGameSystemHandler {
    Task<CRUDResult<IEnumerable<Row>>> GetManyAsync(CancellationToken cancellation = default);
    Task<CRUDResult<GameSystem>> GetByIdAsync(Guid id, CancellationToken cancellation = default);
    Task<CRUDResult<GameSystem>> AddAsync(GameSystem input, CancellationToken cancellation = default);
    Task<CRUDResult<GameSystem>> UpdateAsync(GameSystem input, CancellationToken cancellation = default);
    CRUDResult Remove(Guid id);
}