namespace RolePlayReady.Engine.Utilities;

internal class LastProcedureStep : ProcedureStep<TestContext> {
    public LastProcedureStep(IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null)
        : base(stepFactory, loggerFactory) { }
}