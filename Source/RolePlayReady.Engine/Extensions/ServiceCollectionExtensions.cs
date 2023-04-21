namespace RolePlayReady.Engine.Extensions;
public static class ServiceCollectionExtensions {
    public static IServiceCollection AddStepEngine(this IServiceCollection services)
        => services
            .AddSingleton<IStepFactory, StepFactory>()
            .RegisterAllStepsWith<StepFactory>();

    public static IServiceCollection RegisterAllStepsWith<TMarker>(this IServiceCollection services)
        => services.RegisterAllStepsIn(typeof(TMarker).Assembly);

    public static IServiceCollection RegisterAllStepsIn(this IServiceCollection services, Assembly assembly) {
        assembly.GetTypes()
            .Where(a => a.IsAssignableTo(typeof(IRunner)) && a is { IsAbstract: false, IsInterface: false })
            .ToList()
            .ForEach(services.TryAddScoped);
        assembly.GetTypes()
            .Where(a => a.IsAssignableTo(typeof(IStep)) && a is { IsAbstract: false, IsInterface: false })
            .ToList()
            .ForEach(services.TryAddTransient);
        return services;
    }

    public static IServiceCollection AddRunner<TStep>(this IServiceCollection services)
        where TStep : class, IRunner {
        services.TryAddScoped<TStep>();
        return services;
    }

    public static IServiceCollection AddStep<TStep>(this IServiceCollection services)
        where TStep : class, IStep {
        services.TryAddTransient<TStep>();
        return services;
    }
}
