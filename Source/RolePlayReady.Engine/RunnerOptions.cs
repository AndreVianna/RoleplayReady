using RolePlayReady.Engine.Abstractions;

namespace RolePlayReady.Engine;

public class RunnerOptions : IRunnerOptions<RunnerOptions> {
    public string Name => GetType().Name;
}

public class RunnerOptions<TOptions> : RunnerOptions, IRunnerOptions<TOptions>
    where TOptions : RunnerOptions<TOptions> {
}
