namespace RolePlayReady.Handlers;

public interface IDomainHandler {
    Task<Result<IEnumerable<Row>>> GetManyAsync(CancellationToken cancellation = default);
    Task<SearchResult<Domain?>> GetByIdAsync(Guid id, CancellationToken cancellation = default);
    Task<Result<Domain>> AddAsync(Domain input, CancellationToken cancellation = default);
    Task<SearchResult<Domain>> UpdateAsync(Domain input, CancellationToken cancellation = default);
    SearchResult Remove(Guid id);
}