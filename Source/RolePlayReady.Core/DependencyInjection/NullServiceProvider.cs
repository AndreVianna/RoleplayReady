namespace Microsoft.Extensions.DependencyInjection;

public static class NullServiceProvider {
    public static IServiceProvider Instance { get; } = NullServiceCollection.Instance.BuildServiceProvider();
}