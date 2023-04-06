namespace RolePlayReady.Engine;

public class RunnerOptions : IRunnerOptions<RunnerOptions> {
    public string Name { get; set; } = string.Empty;
}

public class RunnerOptions<TOptions> : RunnerOptions, IRunnerOptions<TOptions>
    where TOptions : RunnerOptions<TOptions> {
}
