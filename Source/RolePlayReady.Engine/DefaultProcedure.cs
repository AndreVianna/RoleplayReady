namespace RolePlayReady.Engine;

public class DefaultProcedure : Procedure<EmptyContext> {
    public DefaultProcedure(IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null)
        : base(new(), stepFactory, loggerFactory) {
    }

    public DefaultProcedure(bool throwsOnError, IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null)
        : base(new(throwsOnError), stepFactory, loggerFactory) {
    }

    public DefaultProcedure(EmptyContext context, IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null)
        : base(context, stepFactory, loggerFactory) {
    }

    [SetsRequiredMembers]
    public DefaultProcedure(string name, IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null)
        : base(name, new(), stepFactory, loggerFactory) {
    }

    [SetsRequiredMembers]
    public DefaultProcedure(string name, bool throwsOnError, IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null)
        : base(name, new(throwsOnError), stepFactory, loggerFactory) {
    }

    [SetsRequiredMembers]
    public DefaultProcedure(string name, EmptyContext context, IStepFactory? stepFactory = null, ILoggerFactory? loggerFactory = null)
        : base(name, context, stepFactory, loggerFactory) {
    }
}