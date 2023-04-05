using RolePlayReady.Engine.Steps;

namespace RolePlayReady.Engine.Defaults;

public class DefaultEndStep : EndStep<DefaultContext> {
    public DefaultEndStep(IServiceCollection services, ILoggerFactory? loggerFactory = null)
        : base(services, loggerFactory) {
    }
}