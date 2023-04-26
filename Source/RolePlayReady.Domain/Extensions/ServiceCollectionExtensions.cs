namespace RolePlayReady.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddDomainHandlers(this IServiceCollection services) {
        services.AddScoped<IDomainHandler, DomainHandler>();
        services.AddScoped<IGameSystemHandler, GameSystemHandler>();
        return services;
    }
}