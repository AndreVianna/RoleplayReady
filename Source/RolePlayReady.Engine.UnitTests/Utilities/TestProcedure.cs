namespace RolePlayReady.Engine.Utilities;

internal class TestProcedure : Procedure<EmptyContext> {
    public TestProcedure(IStepFactory stepFactory, ILoggerFactory? loggerFactory = null)
        : base(new(), stepFactory, loggerFactory) { }

    public TestProcedure(EmptyContext context, IStepFactory stepFactory, ILoggerFactory? loggerFactory = null)
        : base(context, stepFactory, loggerFactory) { }

    [SetsRequiredMembers]
    public TestProcedure(string name, EmptyContext context, IStepFactory stepFactory, ILoggerFactory? loggerFactory = null)
        : base(name, context, stepFactory, loggerFactory) { }

    protected override Task<Type?> OnStartAsync(CancellationToken cancellation = default)
        => Task.FromResult(typeof(FirstStep))!;
}