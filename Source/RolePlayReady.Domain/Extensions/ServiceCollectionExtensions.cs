using RolePlayReady.Handlers.Setting;
using RolePlayReady.Handlers.System;

namespace RolePlayReady.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddDomainHandlers(this IServiceCollection services) {
        services.AddScoped<IAuthHandler, AuthHandler>();
        services.AddScoped<ISystemHandler, SystemHandler>();
        services.AddScoped<ISettingHandler, SettingHandler>();
        return services;
    }
}