namespace RolePlayReady.Engine.Abstractions;

public interface IRunnerOptions { }

public interface IRunnerOptions<out TOptions> : IRunnerOptions
    where TOptions : class, IRunnerOptions<TOptions> {
    string Name { get; }
}