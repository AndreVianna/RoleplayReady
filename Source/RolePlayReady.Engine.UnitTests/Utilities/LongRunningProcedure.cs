namespace RolePlayReady.Engine.Utilities;

internal class LongRunningProcedure : DefaultProcedure {
    public LongRunningProcedure(DefaultContext context, IStepFactory stepFactory)
        : base(context, stepFactory) {
    }

    protected override Task OnFinishAsync(CancellationToken cancellation = default)
        => Task.Delay(TimeSpan.FromSeconds(10), cancellation);
}