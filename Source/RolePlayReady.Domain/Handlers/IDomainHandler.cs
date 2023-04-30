namespace RolePlayReady.Handlers;

public interface IDomainHandler {
    Task<Result<IEnumerable<Row>>> GetManyAsync(CancellationToken cancellation = default);
    Task<ResultOrNotFound<Domain>> GetByIdAsync(Guid id, CancellationToken cancellation = default);
    Task<Result<Domain>> AddAsync(Domain input, CancellationToken cancellation = default);
    Task<ResultOrNotFound<Domain>> UpdateAsync(Domain input, CancellationToken cancellation = default);
    ResultOrNotFound Remove(Guid id);
}