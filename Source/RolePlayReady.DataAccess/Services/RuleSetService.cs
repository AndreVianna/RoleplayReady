using Microsoft.Extensions.DependencyInjection;

namespace RolePlayReady.DataAccess.Services;

public class RuleSetService {
    private readonly IServiceProvider _services;

    public RuleSetService(IServiceProvider services) {
        _services = services;
    }

    public async Task<RuleSet> LoadRuleSetAsync(string fileName, CancellationToken cancellation) {
        var context = new ReadJsonContext<RuleSet>(_services, fileName);
        var runner = _services.GetRequiredService<LoadRuleSetFromJson>();
        context = await runner.RunAsync(context, cancellation);
        return context.Result!;
    }
}