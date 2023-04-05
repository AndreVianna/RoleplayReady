namespace RolePlayReady.Engine.Utilities;

internal class TestProcedure : DefaultProcedure {
    public TestProcedure(EmptyContext context, IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null)
        : base(context, stepFactory, loggerFactory) { }

    [SetsRequiredMembers]
    public TestProcedure(string name, EmptyContext context, IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null)
        : base(name, context, stepFactory, loggerFactory) { }

    protected override Task<Type?> OnStartAsync(CancellationToken cancellation = default)
        => Task.FromResult(typeof(FirstStep))!;
}