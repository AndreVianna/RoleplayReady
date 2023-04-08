namespace RolePlayReady.DataAccess.Services;

public class RuleSetService {
    private readonly IRuleSetRepository _ruleSets;

    public RuleSetService(IRuleSetRepository ruleSets) {
        _ruleSets = ruleSets;
    }

    public async Task<RuleSet> LoadAsync(string id, CancellationToken cancellation = default)
        => await _ruleSets.GetByIdAsync(id, cancellation)
           ?? throw new InvalidOperationException($"Rule set for '{id}' was not found.");
}
