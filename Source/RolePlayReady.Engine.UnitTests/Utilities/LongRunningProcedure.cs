namespace RolePlayReady.Engine.Utilities;

internal class LongRunningProcedure : DefaultProcedure {
    public LongRunningProcedure(IServiceCollection services) 
        : base(services) {
    }

    protected override Task OnFinishAsync(CancellationToken cancellation = default)
        => Task.Delay(TimeSpan.FromSeconds(10), cancellation);
}