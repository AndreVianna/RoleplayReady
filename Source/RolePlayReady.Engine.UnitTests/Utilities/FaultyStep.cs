namespace RolePlayReady.Engine.Utilities;

internal class FaultyStep : Step {
    public FaultyStep()
        : this(null, null) { }

    public FaultyStep(IStepFactory? stepFactory, ILoggerFactory? loggerFactory)
        : base(stepFactory, loggerFactory) { }


    protected override Task<Type?> OnRunAsync(CancellationToken cancellation = default)
        => throw new Exception("Some exception.");
}