using RolePlayReady.Security;

namespace RolePlayReady.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddDomainHandlers(this IServiceCollection services) {
        services.AddScoped<IDomainHandler, DomainHandler>();
        services.AddScoped<IGameSystemHandler, GameSystemHandler>();
        return services;
    }
    public static IServiceCollection AddDummyUserAccessor(this IServiceCollection services) {
        services.AddSingleton<IUserAccessor, DummyUserAccessor>();
        return services;
    }
}