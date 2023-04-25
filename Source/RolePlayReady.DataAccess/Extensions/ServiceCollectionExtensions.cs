using RolePlayReady.DataAccess.Repositories;

namespace RolePlayReady.DataAccess.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddRepositories(this IServiceCollection services) {
        services.AddScoped<IDomainRepository, DomainRepository>();
        services.AddScoped<IGameSystemRepository, GameSystemRepository>();
        services.AddScoped(typeof(ITrackedJsonFileRepository<>), typeof(TrackedJsonFileRepository<>));
        return services;
    }
}