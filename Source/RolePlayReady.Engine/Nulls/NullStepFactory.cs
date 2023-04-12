using RolePlayReady.Engine.Steps.Abstractions;

namespace RolePlayReady.Engine.Nulls;

public sealed class NullStepFactory : IStepFactory {
    private NullStepFactory() { }

    public static NullStepFactory Instance { get; } = new();

    public IStep Create(Type stepType) => NullStep.Instance;
}