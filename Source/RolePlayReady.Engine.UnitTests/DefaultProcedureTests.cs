namespace RolePlayReady.Engine;

public class DefaultProcedureTests {
    [Fact]
    public void Constructor_WithNoParameters_CreatesDefaultProcedure() {
        var procedure = new DefaultProcedure();
        procedure.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_WithEmptyContext_CreatesDefaultProcedure() {
        var context = new EmptyContext();
        var procedure = new DefaultProcedure(context);
        procedure.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_WithName_CreatesDefaultProcedure() {
        var procedure = new DefaultProcedure("TestProcedure");
        procedure.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_WithNameAndEmptyContext_CreatesDefaultProcedure() {
        var context = new EmptyContext();
        var procedure = new DefaultProcedure("TestProcedure", context);
        procedure.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_WithStepFactory_CreatesDefaultProcedure() {
        var stepFactory = new StepFactory();
        var procedure = new DefaultProcedure(stepFactory: stepFactory);
        procedure.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_WithLoggerFactory_CreatesDefaultProcedure() {
        var loggerFactory = NullLoggerFactory.Instance;
        var procedure = new DefaultProcedure(loggerFactory: loggerFactory);
        procedure.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_WithStepFactoryAndLoggerFactory_CreatesDefaultProcedure() {
        var stepFactory = new StepFactory();
        var loggerFactory = NullLoggerFactory.Instance;
        var procedure = new DefaultProcedure(stepFactory: stepFactory, loggerFactory: loggerFactory);
        procedure.Should().NotBeNull();
    }
}