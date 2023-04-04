namespace RolePlayReady.Engine.Utilities;

internal class LongRunningProcedure : Procedure<TestContext, TestProcedure> {
    public LongRunningProcedure(TestContext context, Type initialStep, IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null)
        : base(context, initialStep, stepFactory, loggerFactory) { }

    protected override Task OnFinishAsync(CancellationToken cancellation = default)
        => Task.Delay(TimeSpan.FromSeconds(10), cancellation);
}