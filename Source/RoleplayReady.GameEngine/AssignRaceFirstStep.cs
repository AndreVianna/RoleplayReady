namespace RoleplayReady.GameEngine;

public class AssignRaceFirstStep : Step<AgentContext> {
    public AssignRaceFirstStep(IStepFactory stepFactory, ILoggerFactory? loggerFactory = null)
        : base(stepFactory, loggerFactory) {
    }

    protected override Task<Type?> OnRunAsync(AgentContext context, CancellationToken cancellation = default) {
        // Apply the rule to the character in the context
        // ...

        // Return next rule step type or null if it's the last one
        // ...
        return Task.FromResult<Type?>(default);
    }
}