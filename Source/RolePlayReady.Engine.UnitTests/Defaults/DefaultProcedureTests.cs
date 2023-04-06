namespace RolePlayReady.Engine.Defaults;

public class DefaultProcedureTests {
    private readonly ServiceCollection _services;
    private readonly ServiceProvider _provider;
    private readonly StepFactory _stepFactory;

    public DefaultProcedureTests() {
        _services = new();
        _services.AddStepEngine();
        _provider = _services.BuildServiceProvider();
        _stepFactory = new StepFactory(_provider);
    }

    [Fact]
    public void Constructor_WithName_CreatesDefaultProcedure() {
        var context = new DefaultContext(_provider);
        var procedure = new DefaultProcedure("TestProcedure", context, _stepFactory, NullLoggerFactory.Instance);

        procedure.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_WithStepFactoryAndLoggerFactory_CreatesDefaultProcedure() {
        var context = new DefaultContext(_provider);
        var procedure = new DefaultProcedure(context, _stepFactory, NullLoggerFactory.Instance);

        procedure.Should().NotBeNull();
    }
}