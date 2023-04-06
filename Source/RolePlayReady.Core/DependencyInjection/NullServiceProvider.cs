namespace Microsoft.Extensions.DependencyInjection;

public sealed class NullServiceProvider {
    public static IServiceProvider Instance { get; }
        = NullServiceCollection.Instance.BuildServiceProvider();
}