namespace RolePlayReady.Engine.Utilities;

internal class LongRunningProcedure : DefaultProcedure {
    protected override Task OnFinishAsync(CancellationToken cancellation = default)
        => Task.Delay(TimeSpan.FromSeconds(10), cancellation);
}