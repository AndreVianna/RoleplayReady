namespace RolePlayReady.Engine.Utilities;

internal class FaultyProcedureStep : ProcedureStep<TestContext> {
    public FaultyProcedureStep(IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null)
        : base(stepFactory, loggerFactory) {
    }

    protected override Task OnRunAsync(CancellationToken cancellation = default) => throw new Exception("Some exception.");
}