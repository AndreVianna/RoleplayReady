namespace RolePlayReady.Engine.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection RegisterAllStepsWith<TMarker>(this IServiceCollection services) =>
        services.RegisterAllStepsIn(typeof(TMarker).Assembly);

    public static IServiceCollection RegisterAllStepsIn(this IServiceCollection services, Assembly assembly) {
        assembly.GetTypes()
            .Where(a => a.IsAssignableTo(typeof(IStep)) && !a.IsAbstract && !a.IsInterface)
            .ToList()
            .ForEach(services.TryAddTransient);
        return services;
    }

    public static IServiceCollection AddStep<TStep>(this IServiceCollection services)
        where TStep : class, IStep {
        services.TryAddTransient<TStep>();
        return services;
    }

    public static IServiceCollection AddStepEngine(this IServiceCollection services)
        => services
            .AddSingleton<IStepFactory, StepFactory>()
            .RegisterAllStepsWith<StepFactory>();
}
