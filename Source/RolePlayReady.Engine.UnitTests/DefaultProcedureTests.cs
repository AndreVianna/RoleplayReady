namespace RolePlayReady.Engine;

public class DefaultProcedureTests {
    [Fact]
    public void Constructor_WithName_CreatesDefaultProcedure() {
        var stepFactory = NullStepFactory.Instance;
        var loggerFactory = NullLoggerFactory.Instance;
        var procedure = new DefaultProcedure("TestProcedure", stepFactory, loggerFactory);
        procedure.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_WithStepFactoryAndLoggerFactory_CreatesDefaultProcedure() {
        var stepFactory = NullStepFactory.Instance;
        var loggerFactory = NullLoggerFactory.Instance;
        var procedure = new DefaultProcedure(stepFactory, loggerFactory);
        procedure.Should().NotBeNull();
    }
}