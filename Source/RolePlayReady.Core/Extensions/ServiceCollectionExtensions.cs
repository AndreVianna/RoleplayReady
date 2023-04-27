using System.Defaults;

namespace System.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddDefaultSystemProviders(this IServiceCollection services) {
        services.AddSingleton<IDateTime, SystemDateTime>();
        services.AddSingleton<IFileSystem, DefaultFileSystem>();
        return services;
    }
}