namespace RolePlayReady.Handlers;

public interface IDomainHandler {
    Task<CRUDResult<IEnumerable<Row>>> GetManyAsync(CancellationToken cancellation = default);
    Task<CRUDResult<Domain>> GetByIdAsync(Guid id, CancellationToken cancellation = default);
    Task<CRUDResult<Domain>> AddAsync(Domain input, CancellationToken cancellation = default);
    Task<CRUDResult<Domain>> UpdateAsync(Domain input, CancellationToken cancellation = default);
    CRUDResult Remove(Guid id);
}