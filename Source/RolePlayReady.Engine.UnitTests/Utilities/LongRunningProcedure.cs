namespace RolePlayReady.Engine.Utilities;

internal class LongRunningProcedure : DefaultProcedure {
    public LongRunningProcedure(bool throwsOnError = true)
        : base(new EmptyContext(throwsOnError)) { }

    protected override Task OnFinishAsync(CancellationToken cancellation = default)
        => Task.Delay(TimeSpan.FromSeconds(10), cancellation);
}