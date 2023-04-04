namespace RolePlayReady.Engine.Utilities;

internal class FaultyProcedure : Procedure<TestContext, TestProcedure> {
    public FaultyProcedure(TestContext context, Type initialStep, IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null)
        : base(context, initialStep, stepFactory, loggerFactory) { }

    protected override Task OnBeforeStartAsync(CancellationToken cancellation = default) => throw new Exception("Some exception.");
}