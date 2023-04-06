namespace RoleplayReady.GameEngine;

public class AgentRunner : Runner<AgentContext, AgentRunnerOptions>, IAgentRunner {
    [SetsRequiredMembers]
    public AgentRunner(IConfiguration configuration, IStepFactory stepFactory, ILoggerFactory? loggerFactory)
        : base(configuration, stepFactory, loggerFactory) {
    }

    protected override Task<Type?> OnStartAsync(AgentContext context, CancellationToken cancellation = default) {
        var ruleName = context.RuleName;
        // Load the json corresponding to the ruleName, deserialize and add to the list of rules in the context
        
        context.Rules.Add(new GameRule());
        context.Rules.Add(new GameRule());
        // Return first rule step type

        return Task.FromResult<Type?>(typeof(AssignRaceFirstStep));
    }
}