namespace RolePlayReady.Engine.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddStep<TStep>(this IServiceCollection services)
        where TStep : class, IStep {
        services.TryAddTransient<TStep>();
        return services;
    }

    public static IServiceCollection AddEngine(this IServiceCollection services)
        => services
            .AddSingleton<IStepFactory, StepFactory>()
            .AddStep<EndStep>();
}
