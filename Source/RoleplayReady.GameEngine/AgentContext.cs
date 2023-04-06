namespace RoleplayReady.GameEngine;

public class AgentContext : Context {
    public AgentContext(IServiceProvider services, Agent agent, string ruleName) : base(services) {
        Agent = agent;
        RuleName = ruleName;
    }

    public string RuleName { get; }
    public Agent Agent { get; }

    public IList<GameRule> Rules { get; } = new List<GameRule>(); // Associate the rule to its first step.
}