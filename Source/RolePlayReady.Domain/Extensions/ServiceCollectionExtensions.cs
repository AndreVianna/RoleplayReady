namespace RolePlayReady.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddDomainHandlers(this IServiceCollection services) {
        services.AddScoped<IAuthHandler, AuthHandler>();
        services.AddScoped<IGameSystemHandler, GameSystemHandler>();
        services.AddScoped<ISphereHandler, SphereHandler>();
        return services;
    }
}