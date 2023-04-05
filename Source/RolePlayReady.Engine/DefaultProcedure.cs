namespace RolePlayReady.Engine;

public class DefaultProcedure : Procedure<EmptyContext> {
    public DefaultProcedure(IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null)
        : base(new(), stepFactory, loggerFactory) {
    }

    [SetsRequiredMembers]
    public DefaultProcedure(string name, IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null)
        : base(name, new(), stepFactory, loggerFactory) {
    }
}