namespace RolePlayReady.DataAccess.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddRepositories(this IServiceCollection services) {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGameSystemRepository, GameSystemRepository>();
        services.AddScoped<ISphereRepository, SphereRepository>();
        services.AddScoped(typeof(IJsonFileStorage<>), typeof(JsonFileStorage<>));
        return services;
    }
}