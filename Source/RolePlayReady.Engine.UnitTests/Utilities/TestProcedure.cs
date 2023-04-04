namespace RolePlayReady.Engine.Utilities;

internal class TestProcedure : Procedure<TestContext, TestProcedure> {
    public TestProcedure(TestContext context, Type initialStep, IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null)
        : base(context, initialStep, stepFactory, loggerFactory) { }

    [SetsRequiredMembers]
    public TestProcedure(TestContext context, string name, Type initialStep, IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null)
        : base(context, name, initialStep, stepFactory, loggerFactory) { }
}