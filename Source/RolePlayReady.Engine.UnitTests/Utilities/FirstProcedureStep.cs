namespace RolePlayReady.Engine.Utilities;

internal class FirstProcedureStep : ProcedureStep<TestContext> {
    public FirstProcedureStep(IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null)
        : base(stepFactory, loggerFactory) { }

    protected override Task OnRunAsync(CancellationToken cancellation = default) {
        NextStep = typeof(LastProcedureStep);
        return base.OnRunAsync(cancellation);
    }
}