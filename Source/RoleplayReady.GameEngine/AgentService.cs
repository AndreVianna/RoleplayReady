namespace RoleplayReady.GameEngine;

public class AgentService {
    private readonly IAgentRunner _agentRunner;
    private readonly IServiceProvider _services;

    public AgentService(IAgentRunner agentRunner, IServiceProvider services) {
        _agentRunner = agentRunner;
        _services = services;
    }

    public async Task ApplyRulesAsync(Agent agent, string ruleName, CancellationToken cancellation) {
        var context = new AgentContext(_services, agent, ruleName);
        await _agentRunner.RunAsync(context, cancellation);
    }
}