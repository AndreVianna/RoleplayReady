namespace RolePlayReady.DataAccess.Contracts;

public interface IRuleSetRepository {
    Task<IEnumerable<RuleSet>> GetManyAsync(CancellationToken cancellation = default);
    Task<RuleSet?> GetByIdAsync(string id, CancellationToken cancellation = default);
    Task UpsertAsync(RuleSet ruleSet, CancellationToken cancellation = default);
    void Delete(string id);
}