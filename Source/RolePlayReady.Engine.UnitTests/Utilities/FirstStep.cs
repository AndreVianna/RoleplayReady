namespace RolePlayReady.Engine.Utilities;

internal class FirstStep : Step {
    public FirstStep()
        : this(null, null) { }

    public FirstStep(IStepFactory? stepFactory, ILoggerFactory? loggerFactory)
        : base(stepFactory, loggerFactory) { }

    protected override Task<Type?> OnRunAsync(CancellationToken cancellation = default)
        => Task.FromResult((Type?)typeof(EndStep));
}