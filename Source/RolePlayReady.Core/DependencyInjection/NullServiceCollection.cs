namespace Microsoft.Extensions.DependencyInjection;

public static class NullServiceCollection {
    public static IServiceCollection Instance { get; } = new ServiceCollection();
}