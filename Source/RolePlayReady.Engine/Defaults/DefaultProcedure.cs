namespace RolePlayReady.Engine.Defaults;

public class DefaultProcedure : Procedure<DefaultContext> {
    public DefaultProcedure(DefaultContext context, IStepFactory stepFactory, ILoggerFactory? loggerFactory = null)
        : base(context, stepFactory, loggerFactory) {
    }

    [SetsRequiredMembers]
    public DefaultProcedure(string name, DefaultContext context, IStepFactory stepFactory, ILoggerFactory? loggerFactory = null)
        : base(name, context, stepFactory, loggerFactory) {
    }
}