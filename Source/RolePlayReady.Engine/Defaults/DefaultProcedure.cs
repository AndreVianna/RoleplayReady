namespace RolePlayReady.Engine.Defaults;

public class DefaultProcedure : Procedure<DefaultContext> {
    public DefaultProcedure(IServiceCollection services, ILoggerFactory? loggerFactory = null)
        : base(new(), services, loggerFactory) {
    }

    [SetsRequiredMembers]
    public DefaultProcedure(string name, IServiceCollection services, ILoggerFactory? loggerFactory = null)
        : base(name, new(), services, loggerFactory) {
    }
}