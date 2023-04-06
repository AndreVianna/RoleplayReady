namespace Microsoft.Extensions.DependencyInjection;

public sealed class NullServiceCollection {
    public static IServiceCollection Instance { get; }
        = new ServiceCollection();
}