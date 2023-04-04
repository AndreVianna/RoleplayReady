namespace RolePlayReady.Engine.Utilities;

internal class LongRunningProcedureStep : ProcedureStep<TestContext> {
    public LongRunningProcedureStep(IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null)
        : base(stepFactory, loggerFactory) { }

    protected override Task OnRunAsync(CancellationToken cancellation = default)
        => Task.Delay(TimeSpan.FromSeconds(10), cancellation);
}