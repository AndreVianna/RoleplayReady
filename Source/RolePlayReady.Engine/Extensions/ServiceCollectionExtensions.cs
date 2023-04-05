using Microsoft.Extensions.DependencyInjection.Extensions;

namespace RolePlayReady.Engine.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddStep<TStep>(this IServiceCollection services)
        where TStep : class, IStep {
        services.TryAddTransient(p => Create.Instance<TStep>(services, p.GetService<ILoggerFactory>()));
        return services;
    }

    public static IServiceCollection AddEngine(this IServiceCollection services)
        => services
            .AddSingleton<IStepFactory>(p => new StepFactory(services))
            .AddStep<DefaultEndStep>();
}
