namespace RolePlayReady.DataAccess.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddRepositories(this IServiceCollection services) {
        services.AddSingleton<IUserMapper, UserMapper>();
        services.AddSingleton<IGameSystemMapper, GameSystemMapper>();
        services.AddSingleton<ISphereMapper, SphereMapper>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGameSystemRepository, GameSystemRepository>();
        services.AddScoped<ISphereRepository, SphereRepository>();
        services.AddScoped(typeof(IJsonFileHandler<>), typeof(JsonFileHandler<>));
        return services;
    }
}